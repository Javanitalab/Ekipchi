using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IRoleService : IRepository<Role>
    {
        Task<PagedList<Role>> GetRoleAsync(PagingOptions pagingOptions, string query);

        Task<Role> GetRoleAsync(int id);

        Task<bool> IsRoleExist(int id);
    }
}