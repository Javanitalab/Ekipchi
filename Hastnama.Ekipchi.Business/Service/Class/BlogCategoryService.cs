using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.BlogCategory;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class BlogCategoryService : Repository<EkipchiDbContext, BlogCategory>, IBlogCategoryService
    {
        private readonly IMapper _mapper;

        public BlogCategoryService(EkipchiDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<PagedList<BlogCategoryDto>>> List(PagingOptions pagingOptions,
            FilterBlogCategoryQueryDto filterQueryDto)
        {
            var cities = await WhereAsyncAsNoTracking(b =>
                    (string.IsNullOrEmpty(filterQueryDto.Name) ||
                     b.Name.ToLower().Contains(filterQueryDto.Name.ToLower())
                     && (string.IsNullOrEmpty(filterQueryDto.Slug) ||
                         b.Slug.ToLower().Contains(filterQueryDto.Slug))), pagingOptions,
                b => b.ParentCategory);

            return Result<PagedList<BlogCategoryDto>>.SuccessFull(cities.MapTo<BlogCategoryDto>(_mapper));
        }

        public async Task<Result> Update(UpdateBlogCategoryDto updateBlogCategoryDto)
        {
            var duplicateBlogCategory =
                await FirstOrDefaultAsyncAsNoTracking(c => c.Name == updateBlogCategoryDto.Name);
            if (duplicateBlogCategory != null)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.DuplicateBlogCategoryName}));

            var blogCategory = await FirstOrDefaultAsync(c => c.Id == updateBlogCategoryDto.Id);
            _mapper.Map(updateBlogCategoryDto, blogCategory);
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }

        public async Task<Result<BlogCategoryDto>> Create(CreateBlogCategoryDto createBlogCategoryDto)
        {
            var duplicateBlogCategory =
                await FirstOrDefaultAsyncAsNoTracking(c => c.Name == createBlogCategoryDto.Name);
            if (duplicateBlogCategory != null)
                return Result<BlogCategoryDto>.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.DuplicateBlogCategoryName}));

            var blogCategory = _mapper.Map(createBlogCategoryDto, new BlogCategory());
            await AddAsync(blogCategory);
            await Context.SaveChangesAsync();

            return Result<BlogCategoryDto>.SuccessFull(_mapper.Map<BlogCategoryDto>(blogCategory));
        }

        public async Task<Result<BlogCategoryDto>> Get(int id)
        {
            var blogCategory = await FirstOrDefaultAsyncAsNoTracking(c => c.Id == id, c => c.ParentCategory);
            if (blogCategory == null)
                return Result<BlogCategoryDto>.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = PersianErrorMessage.BlogCategoryNotFound}));

            return Result<BlogCategoryDto>.SuccessFull(_mapper.Map<BlogCategoryDto>(blogCategory));
        }
    }
}