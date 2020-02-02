using System;
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
using Hastnama.GuitarIranShop.DataAccess.Helper;
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
            var user = await FirstOrDefaultAsyncAsNoTracking(u =>
                (string.IsNullOrEmpty(loginDto.Username) || u.Username == loginDto.Username)
                && (string.IsNullOrEmpty(loginDto.Email) || u.Email == loginDto.Email)
                && (string.IsNullOrEmpty(loginDto.Mobile) || u.Mobile == loginDto.Mobile));

            if (user == null)
                return Result<User>.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.UserNotFound}));

            if (!StringUtil.CheckPassword(loginDto.Password, user.Password))
                return Result<User>.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.InvalidUserCredential}));

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

        public async Task<Result<PagedList<UserDto>>> List(PagingOptions pagingOptions,
            FilterUserQueryDto filterQueryDto)
        {
            var users = await WhereAsyncAsNoTracking(u =>
                    u.Status != UserStatus.Delete
                    && (
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
                return Result<PagedList<UserDto>>.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.BadRequestQuery}));

            return Result<PagedList<UserDto>>.SuccessFull(users.MapTo<UserDto>(_mapper));
        }

        public async Task<Result> UpdateProfile(UpdateUserDto updateUserDto)
        {
            var user = await FirstOrDefaultAsync(u => u.Id == updateUserDto.Id);
            if (user == null)
                return Result.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.UserNotFound}));

            _mapper.Map(updateUserDto, user);
            if (!string.IsNullOrEmpty(updateUserDto.Password))
                user.Password = StringUtil.HashPass(updateUserDto.Password);

            await Context.SaveChangesAsync();
            return Result.SuccessFull();
        }

        public async Task<Result<UserDto>> Create(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            if (user.Email != null)
            {
                var duplicateUser = (await FirstOrDefaultAsyncAsNoTracking(u => u.Email == user.Email));
                if (duplicateUser != null)
                    return Result<UserDto>.Failed(new BadRequestObjectResult(new ApiMessage
                        {Message = PersianErrorMessage.EmailAddressAlreadyExist}));
            }

            if (user.Mobile != null)
            {
                var duplicateUser = (await FirstOrDefaultAsyncAsNoTracking(u => u.Mobile == user.Mobile));
                if (duplicateUser != null)
                    return Result<UserDto>.Failed(new BadRequestObjectResult(new ApiMessage
                        {Message = PersianErrorMessage.MobileAlreadyExist}));
            }

            if (user.Username != null)
            {
                var duplicateUser = (await FirstOrDefaultAsyncAsNoTracking(u => u.Username == user.Username));
                if (duplicateUser != null)
                    return Result<UserDto>.Failed(new BadRequestObjectResult(new ApiMessage
                        {Message = PersianErrorMessage.UsernameAlreadyExist}));
            }

            await AddAsync(user);
            await Context.SaveChangesAsync();
            return Result<UserDto>.SuccessFull(_mapper.Map<UserDto>(user));
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

        public async Task<Result> UpdateStatus(Guid id, UserStatus userStatus)
        {
            if (id == Guid.Empty)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.InvalidUserId}));

            var user = await FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return Result.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.UserNotFound}));
            user.Status = userStatus;

            await Context.SaveChangesAsync();
            return Result.SuccessFull();
        }
    }
}