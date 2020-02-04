using System.Collections.Generic;
using System.Threading.Tasks;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IPermissionService : IRepository<Permission>
    {
        Task<List<Permission>> GetPermissionListAsync();

        Task<bool> HasPermissionExist(int permissionId);
    }
}