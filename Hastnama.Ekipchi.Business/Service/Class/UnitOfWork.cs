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
        #endregion

        public UnitOfWork(EkipchiDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Services

        public IUserService UserService => _userService = _userService ?? new UserService(_context,_mapper);
        
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