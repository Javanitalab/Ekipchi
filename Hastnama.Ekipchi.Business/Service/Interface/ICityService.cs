using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.City;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface ICityService : IRepository<City>
    {
        Task<Result<PagedList<CityDto>>> List(FilterCityQueryDto filterQueryDto);
        Task<Result> Update(UpdateCityDto updateCityDto);
        Task<Result<CityDto>> Create(CreateCityDto dto);
        Task<Result<CityDto>> Get(int id);
    }
}