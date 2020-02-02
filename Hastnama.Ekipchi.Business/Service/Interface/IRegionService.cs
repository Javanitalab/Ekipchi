using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Region;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;


namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IRegionService : IRepository<Region>
    {
        Task<Result<PagedList<RegionDto>>> List(PagingOptions pagingOptions, FilterRegionQueryDto filterQueryDto);
        Task<Result> Update(UpdateRegionDto updateRegionDto);
        Task<Result<RegionDto>> Create(CreateRegionDto dto);
        Task<Result<RegionDto>> Get(int id);
    }
}