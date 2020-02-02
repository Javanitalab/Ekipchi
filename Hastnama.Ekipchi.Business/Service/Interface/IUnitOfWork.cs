using System;
using System.Threading.Tasks;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        #region Services

        IUserService UserService { get; }
        ICityService CityService { get; }
        ICountyService CountyService { get; }
        IProvinceService ProvinceService { get; }
        IEventService EventService { get; }

        #endregion Services

        Task SaveChangesAsync();
    }
}