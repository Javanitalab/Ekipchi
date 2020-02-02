using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Province;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Hastnama.GuitarIranShop.DataAccess.Helper;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IProvinceService : IRepository<Province>
    {
        Task<Result<PagedList<ProvinceDto>>> List(PagingOptions pagingOptions, FilterProvinceQueryDto filterQueryDto);
        Task<Result> Update(UpdateProvinceDto updateProvinceDto);
        Task<Result<ProvinceDto>> Create(CreateProvinceDto dto);
        Task<Result<ProvinceDto>> Get(int id);
    }
}