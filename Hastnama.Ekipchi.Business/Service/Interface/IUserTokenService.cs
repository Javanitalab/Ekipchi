using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
#pragma warning disable 108,114

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IUserTokenService : IRepository<UserToken>
    {
        Task<UserToken> GetUserTokenAsync(string refreshToken);
        Task<UserToken> GetUserTokenAsync(Guid userId);
        Task<UserToken> Add(UserToken userToken);
        Task<Result<IList<UserToken>>> GetAllByUser(Guid userId);
    }
}