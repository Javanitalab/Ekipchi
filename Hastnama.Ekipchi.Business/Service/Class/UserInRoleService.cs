using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class UserInRoleService : Repository<EkipchiDbContext, UserInRole>, IUserInRoleService
    {
        public UserInRoleService(EkipchiDbContext context) : base(context)
        {
        }

        public async Task<List<string>> GetRoleName(Guid userId)
        {
            var roles = await GetAll().Include(x => x.Role).Where(x => x.UserId == userId).ToListAsync();

            List<string> roleName = new List<string>();

            if (roles is null)
                return new List<string>();

            foreach (var role in roles)
                roleName.Add(role.Role.Name);

            return roleName;
        }

        public async Task<List<UserInRole>> GetByRoleId(int roleId)
        {
            return await GetAll().Where(x => x.RoleId == roleId).ToListAsync();
        }

        public async Task<List<UserInRole>> GetByUserId(Guid userId)
        {
            return await GetAll().Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<bool> IsUserAdmin(Guid userId)
        {
            return await GetAll().AnyAsync(x => x.UserId == userId && x.RoleId == Role.Admin);
        }

        public async Task<bool> IsRoleExistInUser(int roleId)
        {
            return await GetAll().AnyAsync(x => x.RoleId == roleId);
        }
    }
}