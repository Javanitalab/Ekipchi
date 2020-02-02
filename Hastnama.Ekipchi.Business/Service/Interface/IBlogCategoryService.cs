using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.BlogCategory;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;


namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IBlogCategoryService : IRepository<BlogCategory>
    {
        Task<Result<PagedList<BlogCategoryDto>>> List(PagingOptions pagingOptions, FilterBlogCategoryQueryDto filterQueryDto);
        Task<Result> Update(UpdateBlogCategoryDto updateBlogCategoryDto);
        Task<Result<BlogCategoryDto>> Create(CreateBlogCategoryDto dto);
        Task<Result<BlogCategoryDto>> Get(int id);
    }
}