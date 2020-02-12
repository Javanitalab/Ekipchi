using System;
using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Blog;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class BlogService : Repository<EkipchiDbContext, Blog>, IBlogService
    {
        private readonly IMapper _mapper;

        public BlogService(EkipchiDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<PagedList<BlogDto>>> List(FilterBlogQueryDto filterQueryDto)
        {
            var blogList = await WhereAsyncAsNoTracking(b =>
                    (string.IsNullOrEmpty(filterQueryDto.Keyword) ||
                     b.Title.ToLower().Contains(filterQueryDto.Keyword.ToLower())
                     && (filterQueryDto.UserId == null ||
                         b.UseId == filterQueryDto.UserId)), filterQueryDto,
                b => b.BlogCategory, b => b.User);

            return Result<PagedList<BlogDto>>.SuccessFull(blogList.MapTo<BlogDto>(_mapper));
        }

        public async Task<Result> Update(UpdateBlogDto updateBlogDto)
        {

            var blog = await FirstOrDefaultAsync(c => c.Id == updateBlogDto.Id);
            if (blog.BlogCategoryId != updateBlogDto.BlogCategoryId)
            {
                var blogCategory =
                    await Context.BlogCategories.FirstOrDefaultAsync(u => u.Id == updateBlogDto.BlogCategoryId);
                if (blogCategory == null)
                    return Result.Failed(new BadRequestObjectResult(new ApiMessage
                        {Message = PersianErrorMessage.InvalidBlogCategoryId}));
                blog.BlogCategory = blogCategory;
            }

            _mapper.Map(updateBlogDto, blog);
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }

        public async Task<Result<BlogDto>> Create(CreateBlogDto createBlogDto, Guid userId)
        {

            var blogCategory =
                await Context.BlogCategories.FirstOrDefaultAsync(u => u.Id == createBlogDto.BlogCategoryId);
            if (blogCategory == null)
                return Result<BlogDto>.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.InvalidBlogCategoryId}));


            var blog = _mapper.Map(createBlogDto, new Blog());
            blog.BlogCategory = blogCategory;
            blog.UseId = userId;
            
            await AddAsync(blog);
            await Context.SaveChangesAsync();

            return Result<BlogDto>.SuccessFull(_mapper.Map<BlogDto>(blog));
        }

        public async Task<Result<BlogDto>> Get(int id)
        {
            var blog = await FirstOrDefaultAsyncAsNoTracking(c => c.Id == id, c => c.BlogCategory);
            if (blog == null)
                return Result<BlogDto>.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = PersianErrorMessage.BlogNotFound}));

            return Result<BlogDto>.SuccessFull(_mapper.Map<BlogDto>(blog));
        }

        public async Task<Result> Delete(int id)
        {
            var blog = await FirstOrDefaultAsyncAsNoTracking(b => b.Id == id,b=>b.User);
            if (blog == null)
                return Result.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = PersianErrorMessage.BlogNotFound}));

            Delete(blog);
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }
    }
}