using System;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Host;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IHostService : IRepository<Host>
    {
        Task<Result<PagedList<HostDto>>> List(FilterHostQueryDto filterQueryDto);
        Task<Result> Update(UpdateHostDto updateHostDto);
        Task<Result<HostDto>> Create(CreateHostDto dto);
        Task<Result<HostDto>> Get(Guid id);
        Task<Result> Delete(Guid id);
    }
}