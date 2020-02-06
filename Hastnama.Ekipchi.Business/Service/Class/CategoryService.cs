using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.General;
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

        public async Task<Result<PagedList<CategoryDto>>> List(PagingOptions pagingOptions,
            FilterCategoryQueryDto filterQueryDto)
        {
            var categoryList = await WhereAsyncAsNoTracking(c =>
                    (string.IsNullOrEmpty(filterQueryDto.Name) ||
                     c.Name.ToLower().Contains(filterQueryDto.Name.ToLower())), pagingOptions,
                c => c.HostCategories, c => c.Categories);

            return Result<PagedList<CategoryDto>>.SuccessFull(categoryList.MapTo<CategoryDto>(_mapper));
        }

        public async Task<Result> Update(UpdateCategoryDto updateCategoryDto)
        {
            var duplicateCategory = await FirstOrDefaultAsyncAsNoTracking(c => c.Name == updateCategoryDto.Name);
            if (duplicateCategory != null)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.DuplicateCategoryName}));

            var category = await FirstOrDefaultAsync(c => c.Id == updateCategoryDto.Id);
            _mapper.Map(updateCategoryDto, category);
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }

        public async Task<Result<CategoryDto>> Create(CreateCategoryDto createCategoryDto)
        {
            var duplicateCategory = await FirstOrDefaultAsyncAsNoTracking(c => c.Name == createCategoryDto.Name);
            if (duplicateCategory != null)
                return Result<CategoryDto>.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.DuplicateCategoryName}));

            var category = _mapper.Map(createCategoryDto, new Category());
            await AddAsync(category);
            await Context.SaveChangesAsync();

            return Result<CategoryDto>.SuccessFull(_mapper.Map<CategoryDto>(category));
        }

        public async Task<Result<CategoryDto>> Get(int id)
        {
            var category =
                await FirstOrDefaultAsyncAsNoTracking(c => c.Id == id, c => c.HostCategories, c => c.Categories);
            if (category == null)
                return Result<CategoryDto>.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = PersianErrorMessage.CategoryNotFound}));

            return Result<CategoryDto>.SuccessFull(_mapper.Map<CategoryDto>(category));
        }

        public async Task<Result> Delete(int id)
        {
            var category =
                await FirstOrDefaultAsyncAsNoTracking(c => c.Id == id);
            if (category == null)
                return Result.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = PersianErrorMessage.CategoryNotFound}));

            category.IsDeleted = true;
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }
    }
}