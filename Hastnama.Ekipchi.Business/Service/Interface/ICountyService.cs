using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Country;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface ICountyService : IRepository<County>
    {
        Task<Result<PagedList<CountyDto>>> List(PagingOptions pagingOptions, FilterCountyQueryDto filterQueryDto);
        Task<Result> Update(UpdateCountyDto updateCountyDto);
        Task<Result<CountyDto>> Create(CreateCountyDto dto);
        Task<Result<CountyDto>> Get(int id);
    }
}