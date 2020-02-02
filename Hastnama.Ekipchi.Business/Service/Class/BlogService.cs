using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Blog;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class BlogService : Repository<EkipchiDbContext, Blog>, IBlogService
    {
        private readonly IMapper _mapper;

        public BlogService(EkipchiDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<PagedList<BlogDto>>> List(PagingOptions pagingOptions,
            FilterBlogQueryDto filterQueryDto)
        {
            var cities = await WhereAsyncAsNoTracking(b =>
                    (string.IsNullOrEmpty(filterQueryDto.Title) ||
                     b.Title.ToLower().Contains(filterQueryDto.Title.ToLower())
                     && (filterQueryDto.UserId == null ||
                         b.UseId == filterQueryDto.UserId)), pagingOptions,
                b => b.BlogCategory);

            return Result<PagedList<BlogDto>>.SuccessFull(cities.MapTo<BlogDto>(_mapper));
        }

        public async Task<Result> Update(UpdateBlogDto updateBlogDto)
        {
            var duplicateBlog = await FirstOrDefaultAsyncAsNoTracking(c => c.Title == updateBlogDto.Title);
            if (duplicateBlog != null)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.DuplicateBlogTitle}));

            var blog = await FirstOrDefaultAsync(c => c.Id == updateBlogDto.Id);
            _mapper.Map(updateBlogDto, blog);
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }

        public async Task<Result<BlogDto>> Create(CreateBlogDto createBlogDto)
        {
            var duplicateBlog = await FirstOrDefaultAsyncAsNoTracking(c => c.Title == createBlogDto.Title);
            if (duplicateBlog != null)
                return Result<BlogDto>.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.DuplicateBlogTitle}));

            var blog = _mapper.Map(createBlogDto, new Blog());
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
    }
}