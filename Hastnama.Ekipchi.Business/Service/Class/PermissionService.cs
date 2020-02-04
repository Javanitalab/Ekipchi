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
    public class PermissionService : Repository<EkipchiDbContext, Permission>, IPermissionService
    {
        public PermissionService(EkipchiDbContext context) : base(context)
        {
        }

        public async Task<List<Permission>> GetPermissionListAsync()
        {
            return await GetAll().Where(x => x.ParentId == null).Include(x => x.Children).ToListAsync();
        }

        public async Task<bool> HasPermissionExist(int permissionId)
        {
            return await GetAll().AnyAsync(x => x.Id == permissionId);
        }
    }
}