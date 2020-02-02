using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Blog;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Hastnama.GuitarIranShop.DataAccess.Helper;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IBlogService : IRepository<Blog>
    {
        Task<Result<PagedList<BlogDto>>> List(PagingOptions pagingOptions, FilterBlogQueryDto filterQueryDto);
        Task<Result> Update(UpdateBlogDto updateBlogDto);
        Task<Result<BlogDto>> Create(CreateBlogDto dto);
        Task<Result<BlogDto>> Get(int id);
    }
}