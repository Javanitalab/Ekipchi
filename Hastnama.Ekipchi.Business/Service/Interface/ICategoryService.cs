using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Category;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;


namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface ICategoryService : IRepository<Category>
    {
        Task<Result<PagedList<CategoryDto>>> List( FilterCategoryQueryDto filterQueryDto);
        Task<Result> Update(UpdateCategoryDto updateCategoryDto);
        Task<Result<CategoryDto>> Create(CreateCategoryDto dto);
        Task<Result<CategoryDto>> Get(int id);
        Task<Result> Delete(int id);

    }
}