using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IRolePermissionService : IRepository<RolePermission>
    {
        Task<List<string>> GetPermission(Guid userId);

        Task<List<RolePermission>> GetRolePermissionListAsync(int roleId);
    }
}