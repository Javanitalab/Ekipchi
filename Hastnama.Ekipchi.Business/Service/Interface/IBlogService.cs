using System;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Blog;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;


namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IBlogService : IRepository<Blog>
    {
        Task<Result<PagedList<BlogDto>>> List(FilterBlogQueryDto filterQueryDto);
        Task<Result> Update(UpdateBlogDto updateBlogDto);

        Task<Result> ChangePublishStatus(int id);
        Task<Result<BlogDto>> Create(CreateBlogDto dto, Guid userId);
        Task<Result<BlogDto>> Get(int id);
        Task<Result> Delete(int id);
    }
}