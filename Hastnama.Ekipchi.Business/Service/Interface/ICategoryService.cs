using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Category;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Hastnama.GuitarIranShop.DataAccess.Helper;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface ICategoryService : IRepository<Category>
    {
        Task<Result<PagedList<CategoryDto>>> List(PagingOptions pagingOptions, FilterCategoryQueryDto filterQueryDto);
        Task<Result> Update(UpdateCategoryDto updateCategoryDto);
        Task<Result<CategoryDto>> Create(CreateCategoryDto dto);
        Task<Result<CategoryDto>> Get(int id);
    }
}