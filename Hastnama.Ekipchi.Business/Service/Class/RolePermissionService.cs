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
    public class RolePermissionService : Repository<EkipchiDbContext, RolePermission>, IRolePermissionService
    {
        public RolePermissionService(EkipchiDbContext context) : base(context)
        {
        }

        public async Task<List<string>> GetPermission(Guid userId)
        {
            var rolePermissions = await GetAll().Include(x => x.Permission)
                .Include(x => x.Role).ThenInclude(x => x.UserInRoles)
                .Where(x => x.Role.UserInRoles.Any(u => u.UserId == userId)).ToListAsync();

            if (rolePermissions is null)
                return new List<string>();

            List<string> permissions = new List<string>();

            foreach (var rolePermission in rolePermissions)
                permissions.Add(rolePermission.Permission.Name);

            return permissions;
        }

        public async Task<List<RolePermission>> GetRolePermissionListAsync(int roleId)
        {
            return await GetAll().Where(x => x.RoleId == roleId).Include(x => x.Permission).ToListAsync();
        }
    }
}