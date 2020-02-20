using System;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.User;
using Hastnama.Ekipchi.Data.User.Wallet;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IUserWalletService : IRepository<UserWallet>
    {
        Task<Result<UserWalletDto>> Get(Guid id);

        Task<Result<UserWalletDto>> CreateNew(Guid userId);

        Task<Result<UserDto>> Update(Guid userId, UserWalletDto userWalletDto);
    }
}