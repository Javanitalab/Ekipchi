using System;
using System.Threading.Tasks;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        #region Services

        IUserService UserService { get; }

        #endregion Services

        Task SaveChangesAsync();
    }
}