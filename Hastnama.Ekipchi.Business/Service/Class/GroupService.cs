using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Group;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class GroupService : Repository<EkipchiDbContext, Group>, IGroupService
    {
        private readonly IMapper _mapper;

        public GroupService(EkipchiDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<PagedList<GroupDto>>> List(PagingOptions pagingOptions,
            FilterGroupQueryDto filterQueryDto)
        {
            var groups = await WhereAsyncAsNoTracking(c =>
                (string.IsNullOrEmpty(filterQueryDto.Name) ||
                 c.Name.ToLower().Contains(filterQueryDto.Name.ToLower())), pagingOptions);


            return Result<PagedList<GroupDto>>.SuccessFull(groups.MapTo<GroupDto>(_mapper));
        }

        public async Task<Result> Update(UpdateGroupDto updateGroupDto)
        {
            var group = await FirstOrDefaultAsync(c => c.Id == updateGroupDto.Id);
            if (group.User.Id != updateGroupDto.User.Id)
            {
                var user = await Context.Users.FirstOrDefaultAsync(u => u.Id == updateGroupDto.User.Id);
                if (user == null)
                    return Result.Failed(new NotFoundObjectResult(
                        new ApiMessage
                            {Message = PersianErrorMessage.UserNotFound}));
                group.User = user;
            }
            
            _mapper.Map(updateGroupDto, group);
            
            if (!group.UserInGroups.Select(g => g.UserId).SequenceEqual(updateGroupDto.UsersInGroup.Select(g => g.Id)))
            {
                // get all users that are removed 
                var removedUsers = group.UserInGroups
                    .Where(user => !updateGroupDto.UsersInGroup.Select(u => u.Id).Contains(user.UserId));
                Context.UserInGroups.RemoveRange(removedUsers);

                // get all users id that are added
                var usersId = updateGroupDto.UsersInGroup.Select(u => u.Id).Where(userId =>
                    !group.UserInGroups.Select(u => u.UserId).Contains(userId));

                var addedUsers = Context.Users.Where(u => usersId.Contains(u.Id)).ToListAsync();
                var userInGroups = addedUsers.Result.Select(user => new UserInGroup
                        {Id = Guid.NewGuid(), Groups = @group, JoinGroupDate = DateTime.Now, User = user})
                    .Union(group.UserInGroups.Where(ug => !removedUsers.Contains(ug))).ToList();
                group.UserInGroups = userInGroups;
            }

            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }

        public async Task<Result<GroupDto>> Create(CreateGroupDto createGroupDto)
        {
            var owner = await Context.Users.FirstOrDefaultAsync(u => u.Id == createGroupDto.User.Id);
            if (owner == null)
                return Result<GroupDto>.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = PersianErrorMessage.UserNotFound}));

            var group = _mapper.Map<Group>(createGroupDto);
            group.User = owner;

            await AddAsync(group);
            await Context.SaveChangesAsync();

            return Result<GroupDto>.SuccessFull(_mapper.Map<GroupDto>(group));
        }

        public async Task<Result<GroupDto>> Get(Guid id)
        {
            var group = await FirstOrDefaultAsyncAsNoTracking(c => c.Id == id);
            if (group == null)
                return Result<GroupDto>.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = PersianErrorMessage.GroupNotFound}));

            return Result<GroupDto>.SuccessFull(_mapper.Map<GroupDto>(group));
        }

        public async Task<Result> Delete(Guid id)
        {
            var group = await FirstOrDefaultAsyncAsNoTracking(c => c.Id == id, g => g.UserInGroups);
            if (group == null)
                return Result.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = PersianErrorMessage.GroupNotFound}));

            Context.UserInGroups.RemoveRange(group.UserInGroups);
            Delete(group);
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }
    }
}