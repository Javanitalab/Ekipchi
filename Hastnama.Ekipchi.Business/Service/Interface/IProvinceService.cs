using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Province;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IProvinceService : IRepository<Province>
    {
        Task<Result<PagedList<ProvinceDto>>> List( FilterProvinceQueryDto filterQueryDto);
        Task<Result> Update(UpdateProvinceDto updateProvinceDto);
        Task<Result<ProvinceDto>> Create(CreateProvinceDto dto);
        Task<Result<ProvinceDto>> Get(int id);
    }
}