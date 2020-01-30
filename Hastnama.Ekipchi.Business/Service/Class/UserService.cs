using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.Enum;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Common.Util;
using Hastnama.Ekipchi.Data.Auth;
using Hastnama.Ekipchi.Data.User;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class UserService : Repository<EkipchiDbContext, User>, IUserService
    {
        private readonly IMapper _mapper;

        public UserService(EkipchiDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<User>> Login(LoginDto loginDto)
        {
            var hashPassword = StringUtil.HashPass(loginDto.Password);
            var user = await FirstOrDefaultAsyncAsNoTracking(u =>
                (string.IsNullOrEmpty(u.Username) || u.Username == loginDto.Username)
                && (string.IsNullOrEmpty(u.Email) || u.Email == loginDto.Email)
                && (string.IsNullOrEmpty(u.Mobile) || u.Mobile == loginDto.Mobile)
                && u.Password == hashPassword);

            if (user == null)
                return Result<User>.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.UserNotFound}));

            if (user.Status != UserStatus.Active)
                return Result<User>.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.UserIsDeActive}));

            return Result<User>.SuccessFull(user);
        }

        public async Task<Result<User>> Register(RegisterDto registerDto)
        {
            var user = await FirstOrDefaultAsyncAsNoTracking(u =>
                (string.IsNullOrEmpty(u.Username) || u.Username == registerDto.Username)
                && (string.IsNullOrEmpty(u.Email) || u.Email == registerDto.Email)
                && (string.IsNullOrEmpty(u.Mobile) || u.Mobile == registerDto.Mobile));

            if (user != null)
                return Result<User>.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.UserAlreadyExist}));

            user = _mapper.Map<User>(registerDto);
            await AddAsync(user);
            await Context.SaveChangesAsync();

            return Result<User>.SuccessFull(user);
        }

        public async Task<Result<User>> GetAsync(Guid id)
        {
            var user = await FirstOrDefaultAsyncAsNoTracking(u => u.Id == id && u.Status == UserStatus.Active);

            if (user == null)
                return Result<User>.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.UserNotFound}));
            return Result<User>.SuccessFull(user);
        }

        public async Task<Result<List<UserDto>>> List(PagingOptions pagingOptions,
            UserFilterQueryDto filterQueryDto)
        {
            var users = await WhereAsyncAsNoTracking(u =>
                    (
                        filterQueryDto.Keyword == null
                        || u.Name.ToLower().Contains(filterQueryDto.Keyword)
                        || u.Family.ToLower().Contains(filterQueryDto.Keyword)
                        || u.Username.ToLower().Contains(filterQueryDto.Keyword)
                        || u.Mobile.ToLower().Contains(filterQueryDto.Keyword)
                        || u.Email.ToLower().Contains(filterQueryDto.Keyword))
                    && (filterQueryDto.Role == null || u.Role == filterQueryDto.Role)
                    && (filterQueryDto.Status == null || u.Status == filterQueryDto.Status)
                , pagingOptions
            );
            if (users == null)
                return Result<List<UserDto>>.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.BadRequestQuery}));

            var dtos = users.Select(user => _mapper.Map<UserDto>(user)).ToList();
            return Result<List<UserDto>>.SuccessFull(dtos);
        }

        public async Task<Result> UpdateProfile(UpdateUserDto updateUserDto)
        {
            var user = await FirstOrDefaultAsync(u => u.Id == updateUserDto.Id);
            if (user == null)
                return Result.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.UserNotFound}));
            _mapper.Map(updateUserDto, user);
            await Context.SaveChangesAsync();
            return Result.SuccessFull();
        }

        public async Task<Result> Create(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            if (user.Email != null)
            {
                var duplicateUser = (await FirstOrDefaultAsyncAsNoTracking(u => u.Email == user.Email));
                if (duplicateUser != null)
                    return Result.Failed(new BadRequestObjectResult(new ApiMessage
                        {Message = PersianErrorMessage.EmailAddressAlreadyExist}));
            }

            if (user.Mobile != null)
            {
                var duplicateUser = (await FirstOrDefaultAsyncAsNoTracking(u => u.Mobile == user.Mobile));
                if (duplicateUser != null)
                    return Result.Failed(new BadRequestObjectResult(new ApiMessage
                        {Message = PersianErrorMessage.MobileAlreadyExist}));
            }

            if (user.Username != null)
            {
                var duplicateUser = (await FirstOrDefaultAsyncAsNoTracking(u => u.Username == user.Username));
                if (duplicateUser != null)
                    return Result.Failed(new BadRequestObjectResult(new ApiMessage
                        {Message = PersianErrorMessage.UsernameAlreadyExist}));
            }

            await AddAsync(user);
            await Context.SaveChangesAsync();
            return Result.SuccessFull();
        }

        public async Task<Result<UserDto>> Get(Guid id)
        {
            if (id == Guid.Empty)
                return Result<UserDto>.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.InvalidUserId}));

            var user = await FirstOrDefaultAsyncAsNoTracking(u => u.Id == id);
            if (user == null)
                return Result<UserDto>.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.UserNotFound}));

            var dto = _mapper.Map<UserDto>(user);
            return Result<UserDto>.SuccessFull(dto);
        }
    }
}