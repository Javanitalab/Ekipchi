using System;
using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.User;
using Hastnama.Ekipchi.Data.User.Wallet;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class UserWalletService : Repository<EkipchiDbContext, UserWallet>, IUserWalletService
    {
        private readonly IMapper _mapper;

        public UserWalletService(EkipchiDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<UserWalletDto>> Get(Guid id)
        {
            var user = await Context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return Result<UserWalletDto>.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = ResponseMessage.UserNotFound}));
            
            return Result<UserWalletDto>.SuccessFull(_mapper.Map<UserWalletDto>(user.UserWallet));
        }

        public async Task<Result<UserWalletDto>> CreateNew(Guid userId)
        {
            var user = await Context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return Result<UserWalletDto>.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = ResponseMessage.UserNotFound}));

            var userWallet = new UserWallet {User = user};
            await Context.SaveChangesAsync();
            
            return Result<UserWalletDto>.SuccessFull(_mapper.Map<UserWalletDto>(userWallet));
        }

        public async Task<Result<UserDto>> Update(Guid userId, UserWalletDto userWalletDto)
        {
            var user = await Context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return Result<UserDto>.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = ResponseMessage.UserNotFound}));
            
            user.UserWallet = _mapper.Map<UserWallet>(userWalletDto);
            await Context.SaveChangesAsync();
            
            return Result<UserDto>.SuccessFull(_mapper.Map<UserDto>(user));
        }
    }
}