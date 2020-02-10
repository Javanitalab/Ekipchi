using System;
using System.Linq;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class UserTokenService : Repository<EkipchiDbContext, UserToken>, IUserTokenService
    {
        public UserTokenService(EkipchiDbContext context) : base(context)
        {
        }
        public async Task<UserToken> GetUserTokenAsync(string refreshToken)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.Token == refreshToken);
        }

        public async Task<UserToken> GetUserTokenAsync(Guid userId)
        {
            return await GetAll().OrderBy(x => x.CreateDateTime).LastOrDefaultAsync(x =>
                x.IsUsed == false && x.UserId == userId && x.ExpiredDate > DateTime.Today);
        }

        public async Task<UserToken> Add(UserToken userToken)
        {
            await AddAsync(userToken);
            await Context.SaveChangesAsync();
            return userToken;
        }
    }
}