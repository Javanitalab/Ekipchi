using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Category;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class CategoryService : Repository<EkipchiDbContext, Category>, ICategoryService
    {
        private readonly IMapper _mapper;

        public CategoryService(EkipchiDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<PagedList<CategoryDto>>> List(FilterCategoryQueryDto filterQueryDto)
        {
            var categoryList = await WhereAsyncAsNoTracking(c => c.IsDeleted == false &&
                                                                 (string.IsNullOrEmpty(filterQueryDto.Keyword) ||
                                                                  c.Name.ToLower()
                                                                      .Contains(filterQueryDto.Keyword.ToLower())),
                filterQueryDto,
                c => c.HostCategories, c => c.Categories);

            return Result<PagedList<CategoryDto>>.SuccessFull(categoryList.MapTo<CategoryDto>(_mapper));
        }

        public async Task<Result> Update(UpdateCategoryDto updateCategoryDto)
        {
            var category = await FirstOrDefaultAsync(c => c.Id == updateCategoryDto.Id);
            if (category == null)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = ResponseMessage.InvalidCategoryId}));

            var duplicatedCategory =
                await FirstOrDefaultAsyncAsNoTracking(c =>
                    c.Name == updateCategoryDto.Name && c.Id != updateCategoryDto.Id);
            if (duplicatedCategory != null)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = ResponseMessage.DuplicateCategoryName}));

            _mapper.Map(updateCategoryDto, category);
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }

        public async Task<Result<CategoryDto>> Create(CreateCategoryDto createCategoryDto)
        {
            var duplicatedCategory = await FirstOrDefaultAsyncAsNoTracking(c => c.Name == createCategoryDto.Name);
            if (duplicatedCategory != null)
                return Result<CategoryDto>.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = ResponseMessage.DuplicateCategoryName}));
            var category = _mapper.Map(createCategoryDto, new Category());
            await AddAsync(category);
            await Context.SaveChangesAsync();

            return Result<CategoryDto>.SuccessFull(_mapper.Map<CategoryDto>(category));
        }

        public async Task<Result<CategoryDto>> Get(int id)
        {
            var category =
                await FirstOrDefaultAsyncAsNoTracking(c => c.IsDeleted == false && c.Id == id, c => c.HostCategories,
                    c => c.Categories);
            if (category == null)
                return Result<CategoryDto>.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = ResponseMessage.CategoryNotFound}));

            return Result<CategoryDto>.SuccessFull(_mapper.Map<CategoryDto>(category));
        }

        public async Task<Result> Delete(int id)
        {
            var category =
                await FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
                return Result.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = ResponseMessage.CategoryNotFound}));

            category.IsDeleted = true;
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }
    }
}