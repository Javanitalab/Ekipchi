using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.DataAccess.Context;

namespace Hastnama.Ekipchi.Business.Service.Class
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly EkipchiDbContext _context;
        private readonly IMapper _mapper;
        private bool _disposed;

        #region private

        private IUserService _userService;
        private ICityService _cityService;
        private ICountyService _countyService;
        private IProvinceService _provinceService;
        private IEventService _eventService;
        #endregion

        public UnitOfWork(EkipchiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Services

        public IUserService UserService => _userService = _userService ?? new UserService(_context, _mapper);
        public ICityService CityService => _cityService = _cityService ?? new CityService(_context, _mapper);
        public ICountyService CountyService => _countyService = _countyService ?? new CountyService(_context, _mapper);
        public IProvinceService ProvinceService => _provinceService = _provinceService ?? new ProvinceService(_context, _mapper);

        public IEventService EventService => _eventService = _eventService ?? new EventService(_context, _mapper);

        #endregion Services

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}