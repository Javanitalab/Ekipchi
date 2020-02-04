using AutoMapper;
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
                    (string.IsNullOrEmpty(filterQueryDto.Name) || e.Name.ToLower().Contains(filterQueryDto.Name.ToLower())
                     && (string.IsNullOrEmpty(filterQueryDto.HostName) || e.Host.Name.ToLower().Contains(filterQueryDto.HostName.ToLower()))) && !e.IsDeleted, pagingOptions,
                e => e.Host, e => e.Category, e => e.EventAccessibility, e => e.EventGallery, e => e.EventSchedule, e => e.EventType);

            return Result<PagedList<EventDto>>.SuccessFull(events.MapTo<EventDto>(_mapper));
        }

        public async Task<Result<EventDto>> Get(Guid id)
        {
            var eventDetail = await FirstOrDefaultAsyncAsNoTracking(x => x.Id == id && !x.IsDeleted);
            if (eventDetail != null)
                return Result<EventDto>.Failed(new NotFoundObjectResult(
                    new ApiMessage
                    { Message = PersianErrorMessage.EventNotFound }));

            return Result<EventDto>.SuccessFull(_mapper.Map<EventDto>(eventDetail));
        }

        public async Task<Result<EventDto>> Create(CreateEventDto createEventDto)
        {
            var newEvent = _mapper.Map<Event>(createEventDto);
            await AddAsync(newEvent);

            foreach (var gallery in createEventDto.CreateEventGallery)
            {
                var galleyItem = _mapper.Map<EventGallery>(gallery);
                galleyItem.EventId = newEvent.Id;
                await Context.EventGalleries.AddAsync(galleyItem);
            }

            var eventSchedule = _mapper.Map<EventSchedule>(createEventDto.CreateEventSchedule);
            eventSchedule.EventId = newEvent.Id;
            await Context.AddAsync(eventSchedule);

            await Context.SaveChangesAsync();

            return Result<EventDto>.SuccessFull(_mapper.Map<EventDto>(newEvent));
        }

        public async Task<Result> Update(UpdateEventDto updateEvent)
        {
            var eventDetail = await FirstOrDefaultAsync(x => x.Id == updateEvent.Id && !x.IsDeleted);

            if (eventDetail is null)
                return Result.Failed(new NotFoundObjectResult(new ApiMessage
                { Message = PersianErrorMessage.EventNotFound }));

            var userEvent = await Context.UserInEvents.Where(x => x.EventId == updateEvent.Id).ToListAsync();
            var userIdList = userEvent.Select(x => x.UserId).ToList();
            if (!userIdList.SequenceEqual(updateEvent.UserList))
            {
                var addedUsers = await Context.Users.Where(x => updateEvent.UserList.Contains(x.Id)).ToListAsync();

                if (addedUsers.Count != updateEvent.UserList.Count)
                    return Result.Failed(new NotFoundObjectResult(new ApiMessage { Message = PersianErrorMessage.UserNotFound }));

                Context.UserInEvents.RemoveRange(userEvent);
            }

            _mapper.Map(updateEvent, eventDetail);
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }

        public async Task<Result> Delete(Guid id)
        {
            var eventDetail = await FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if (eventDetail is null)
                return Result.Failed(new NotFoundObjectResult(new ApiMessage { Message = PersianErrorMessage.EventNotFound }));

            eventDetail.IsDeleted = true;
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }
    }
}