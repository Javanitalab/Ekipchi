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
                     c.Name.ToLower().Contains(filterQueryDto.Name.ToLower())), pagingOptions, g => g.User,
                g => g.UserInGroups.Select(ug => ug.User));


            return Result<PagedList<GroupDto>>.SuccessFull(groups.MapTo<GroupDto>(_mapper));
        }

        public async Task<Result> Update(UpdateGroupDto updateGroupDto)
        {
            var group = await FirstOrDefaultAsync(c => c.Id == updateGroupDto.Id, g => g.UserInGroups, g => g.User);
            if (group.User.Id != updateGroupDto.OwnerId)
            {
                var user = await Context.Users.FirstOrDefaultAsync(u => u.Id == updateGroupDto.OwnerId);
                if (user == null)
                    return Result.Failed(new NotFoundObjectResult(
                        new ApiMessage
                            {Message = PersianErrorMessage.UserNotFound}));
                group.User = user;
            }

            _mapper.Map(updateGroupDto, group);

            if (!group.UserInGroups.Select(g => g.UserId).SequenceEqual(updateGroupDto.UsersInGroup))
            {
                // get all users that are removed 
                var removedUsers = group.UserInGroups
                    .Where(user => !updateGroupDto.UsersInGroup.Contains(user.UserId));
                if (removedUsers.Any())
                    Context.UserInGroups.RemoveRange(removedUsers);

                // get all users id that are added
                var addedUsersId = updateGroupDto.UsersInGroup.Where(userId =>
                    !group.UserInGroups.Select(u => u.UserId).Contains(userId));

                var addedUsers = await Context.Users.Where(u => addedUsersId.Contains(u.Id)).ToListAsync();
                
                // if invalid user id sent 
                if (addedUsers.Count != addedUsersId.Count())
                    return Result.Failed(new BadRequestObjectResult(new ApiMessage
                        {Message = PersianErrorMessage.UserNotFound}));
                
                var userInGroups = addedUsers.Select(user => new UserInGroup
                        {Id = Guid.NewGuid(), Groups = @group, JoinGroupDate = DateTime.Now, User = user})
                    .Union(group.UserInGroups.Where(ug => !removedUsers.Contains(ug))).ToList();
                await Context.UserInGroups.AddRangeAsync(userInGroups);
                group.UserInGroups = userInGroups;
            }

            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }

        public async Task<Result<GroupDto>> Create(CreateGroupDto createGroupDto)
        {
            var owner = await Context.Users.FirstOrDefaultAsync(u => u.Id == createGroupDto.OwnerId);
            if (owner == null)
                return Result<GroupDto>.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = PersianErrorMessage.UserNotFound}));

            var group = _mapper.Map<Group>(createGroupDto);
            group.User = owner;
            group.Members = 1;
            group.Id = Guid.NewGuid();

            await AddAsync(group);
            await Context.SaveChangesAsync();

            return Result<GroupDto>.SuccessFull(_mapper.Map<GroupDto>(group));
        }

        public async Task<Result<GroupDto>> Get(Guid id)
        {
            var group = await FirstOrDefaultAsyncAsNoTracking(c => c.Id == id,
                g => g.UserInGroups.Select(ug => ug.User));
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