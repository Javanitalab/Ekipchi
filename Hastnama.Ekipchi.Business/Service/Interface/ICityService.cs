using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Enum;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Auth;
using Hastnama.Ekipchi.Data.User;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface ICityService : IRepository<City>
    {
        Task<Result<List<UserDto>>> List(PagingOptions pagingOptions, UserFilterQueryDto filterQueryDto);
        Task<Result> Update(UpdateUserDto updateUserDto);
        Task<Result> Create(CreateUserDto dto);
        Task<Result<UserDto>> Get(Guid id);
    }
}