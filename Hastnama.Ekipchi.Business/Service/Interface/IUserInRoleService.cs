using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IUserInRoleService : IRepository<UserInRole>
    {
        Task<List<string>> GetRoleName(Guid userId);

        Task<List<UserInRole>> GetByRoleId(int roleId);

        Task<List<UserInRole>> GetByUserId(Guid userId);

        Task<bool> IsUserAdmin(Guid userId);

        Task<bool> IsRoleExistInUser(int roleId);
    }
}