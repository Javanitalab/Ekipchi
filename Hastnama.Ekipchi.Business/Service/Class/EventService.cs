﻿using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Event;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Helper;
using Microsoft.EntityFrameworkCore;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class EventService : Repository<EkipchiDbContext, Event>, IEventService
    {
        private readonly IMapper _mapper;

        public EventService(EkipchiDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<PagedList<EventDto>>> List(PagingOptions pagingOptions,
            FilterEventQueryDto filterQueryDto)
        {
            var events = await WhereAsyncAsNoTracking(e =>
                    (string.IsNullOrEmpty(filterQueryDto.Name) ||
                     e.Name.ToLower().Contains(filterQueryDto.Name.ToLower())
                     && (string.IsNullOrEmpty(filterQueryDto.HostName) ||
                         e.Host.Name.ToLower().Contains(filterQueryDto.HostName.ToLower()))) && !e.IsDeleted,
                pagingOptions, e => e.Category, e => e.Comment,
                e => e.Host, e => e.UserInEvents.Select(ur => ur.User), e => e.EventGallery.User, e => e.EventSchedule);
            if (events.Items.Any())
            {
                events.Items.ForEach(eventDetail =>
                {
                    if (eventDetail.Comment != null)
                    {
                        eventDetail.Comment = eventDetail.Comment.Where(c => c.IsConfirmed && !c.IsDeleted)
                            .OrderBy(c => c.ModifiedDateTime).ToList();
                    }
                });
            }

            return Result<PagedList<EventDto>>.SuccessFull(events.MapTo<EventDto>(_mapper));
        }

        public async Task<Result<EventDto>> Get(Guid id)
        {
            var eventDetail = await FirstOrDefaultAsyncAsNoTracking(x => x.Id == id && !x.IsDeleted, e => e.Category,
                e => e.Comment,
                e => e.Host, e => e.UserInEvents.Select(ur => ur.User), e => e.EventGallery, e => e.EventSchedule);
            if (eventDetail != null)
                return Result<EventDto>.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = PersianErrorMessage.EventNotFound}));

            return Result<EventDto>.SuccessFull(_mapper.Map<EventDto>(eventDetail));
        }

        public async Task<Result<EventDto>> Create(CreateEventDto createEventDto)
        {
            var category = await Context.Categories.FirstOrDefaultAsync(c => c.Id == createEventDto.CategoryId);
            if (category == null)
                return Result<EventDto>.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.CategoryNotFound}));

            var host = await Context.Hosts.FirstOrDefaultAsync(c => c.Id == createEventDto.HostId);
            if (host == null)
                return Result<EventDto>.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.HostNotFound}));


            var newEvent = _mapper.Map<Event>(createEventDto);
            await AddAsync(newEvent);
            await Context.SaveChangesAsync();

            return Result<EventDto>.SuccessFull(_mapper.Map<EventDto>(newEvent));
        }

        public async Task<Result> Update(UpdateEventDto updateEventDto)
        {
            var category = await Context.Categories.FirstOrDefaultAsync(c => c.Id == updateEventDto.CategoryId);
            if (category == null)
                return Result.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.CategoryNotFound}));

            var host = await Context.Hosts.FirstOrDefaultAsync(c => c.Id == updateEventDto.HostId);
            if (host == null)
                return Result.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.HostNotFound}));


            var eventDetail =
                await FirstOrDefaultAsync(x => x.Id == updateEventDto.Id && !x.IsDeleted
                    , e => e.Category,
                    e => e.Host, e => e.UserInEvents.Select(ur => ur.User), e => e.EventGallery, e => e.EventSchedule,
                    e => e.Comment.Select(c => c.ParentComment), e => e.Comment.Select(c => c.Children));

            if (eventDetail is null)
                return Result.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.EventNotFound}));

            #region Update UserInEvents

            if (!eventDetail.UserInEvents.Select(g => g.UserId).SequenceEqual(updateEventDto.UserInEvents))
            {
                // get all users that are removed 
                var removedUsers = eventDetail.UserInEvents
                    .Where(user => !updateEventDto.UserInEvents.Contains(user.UserId));
                if (removedUsers.Any())
                    Context.UserInEvents.RemoveRange(removedUsers);

                // get all users id that are added
                var addedUsersId = updateEventDto.UserInEvents.Where(userId =>
                    !eventDetail.UserInEvents.Select(u => u.UserId).Contains(userId));

                var addedUsers = await Context.Users.Where(u => addedUsersId.Contains(u.Id)).ToListAsync();

                // if invalid user id sent 
                if (addedUsers.Count != addedUsersId.Count())
                    return Result.Failed(new BadRequestObjectResult(new ApiMessage
                        {Message = PersianErrorMessage.UserNotFound}));

                var userInGroups = addedUsers.Select(user => new UserInEvent
                        {Guid = Guid.NewGuid(), Event = eventDetail, User = user})
                    .Union(eventDetail.UserInEvents.Where(ug => !removedUsers.Contains(ug))).ToList();
                await Context.UserInEvents.AddRangeAsync(userInGroups);
                eventDetail.UserInEvents = userInGroups;
            }

            #endregion

            _mapper.Map(updateEventDto, eventDetail);
            eventDetail.Category = category;
            eventDetail.Host = host;

            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }

        public async Task<Result> Delete(Guid id)
        {
            var eventDetail = await FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if (eventDetail is null)
                return Result.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.EventNotFound}));

            eventDetail.IsDeleted = true;
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }
    }
}