using System.Linq;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class RoleService : Repository<EkipchiDbContext, Role>, IRoleService
    {
        public RoleService(EkipchiDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Role>> GetRoleAsync(PagingOptions pagingOptions, string query)
        {
            IQueryable<Role> roles = GetAll();

            if (!string.IsNullOrWhiteSpace(query))
                roles = roles.Where(x => x.Name.Contains(query));

            switch (pagingOptions.OrderBy)
            {
                case "Name":
                    roles = pagingOptions.Desc ? roles.OrderByDescending(x => x.Name) : roles.OrderBy(x => x.Name);
                    break;

                default:
                    roles = roles.OrderByDescending(x => x.Id);
                    break;
            }

            return await GetPagedAsync(pagingOptions.Page, pagingOptions.Limit, roles);
        }

        public async Task<Role> GetRoleAsync(int id)
        {
            return await GetAll().Include(x => x.RolePermissions)
                .ThenInclude(x => x.Permission).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsRoleExist(int id)
        {
            return await GetAll().AnyAsync(x => x.Id == id);
        }
    }
}