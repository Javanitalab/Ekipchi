﻿using System;
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
    public interface IUserService  : IRepository<User>
    {
        Task<Result<User>> Login(LoginDto registerDto);
        Task<Result<User>> Register(RegisterDto registerDto);
        Task<Result<User>> GetAsync(Guid id);
        Task<Result<List<UserDto>>> List(PagingOptions pagingOptions, UserFilterQueryDto filterQueryDto);
        Task<Result> UpdateProfile(UpdateUserDto updateUserDto);
        Task<Result> Create(CreateUserDto dto);
        Task<Result<UserDto>> Get(Guid id);
        Task<Result> UpdateStatus(Guid id, UserStatus userStatus);
    }
}