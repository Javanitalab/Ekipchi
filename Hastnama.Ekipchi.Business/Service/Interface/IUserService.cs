using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Enum;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Auth;
using Hastnama.Ekipchi.Data.Event;
using Hastnama.Ekipchi.Data.Group;
using Hastnama.Ekipchi.Data.Role;
using Hastnama.Ekipchi.Data.User;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IUserService  : IRepository<User>
    {
        Task<Result<User>> Login(LoginDto registerDto);
        Task<Result<User>> Register(RegisterDto registerDto);
        Task<Result<PagedList<UserDto>>> List( FilterUserQueryDto queryDto);
        Task<Result> UpdateProfile(UpdateUserDto updateUserDto);
        Task<Result<UserDto>> Create(CreateUserDto dto);
        Task<Result<UserDto>> Get(Guid id);
        Task<Result<UserDto>> GetByEmail(string email);
        Task<Result> UpdateStatus(Guid id, UserStatus userStatus);
        Task<Result<IList<GroupDto>>> UserGroups(Guid id);
        Task<Result<IList<EventDto>>> UserEvents(Guid id);
        Task<Result<IList<RoleDto>>> UserRoles(Guid userId);
    }
}