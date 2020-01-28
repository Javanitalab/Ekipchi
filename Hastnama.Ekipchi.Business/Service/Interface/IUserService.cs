using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Auth;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IUserService  : IRepository<User>
    {
        Task<Result<User>> Login(LoginDto registerDto);
        Task<Result<User>> Register(RegisterDto registerDto);
    }
}