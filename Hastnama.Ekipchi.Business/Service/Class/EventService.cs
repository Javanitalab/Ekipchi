using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
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
using Hastnama.Ekipchi.Data.Event.Gallery;
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

        public async Task<Result<PagedList<EventDto>>> List(FilterEventQueryDto filterQueryDto)
        {
            var events = await WhereAsyncAsNoTracking(e =>
                    (string.IsNullOrEmpty(filterQueryDto.Keyword) ||
                     (string.IsNullOrEmpty(e.Name) || e.Name.ToLower().Contains(filterQueryDto.Keyword))
                     || (string.IsNullOrEmpty(e.Host.Name) || e.Host.Name.ToLower().Contains(filterQueryDto.Keyword))
                    ) && !e.IsDeleted,
                filterQueryDto, e => e.Category, e => e.Comment,
                e => e.Host, e => e.UserInEvents.Select(ur => ur.User), e => e.EventGallery.Select(rg => rg.User),
                e => e.EventSchedule);
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
                e => e.Host, e => e.UserInEvents.Select(ur => ur.User), e => e.EventGallery.Select(ug => ug.User),
                e => e.EventSchedule);
            if (eventDetail == null)
                return Result<EventDto>.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = ResponseMessage.EventNotFound}));

            return Result<EventDto>.SuccessFull(_mapper.Map<EventDto>(eventDetail));
        }

        public async Task<Result> UpdateGallery(UpdateEventGalleryDto updateEventGalleryDto)
        {
            var eventGallery = await Context.EventGalleries.FirstOrDefaultAsync(x => x.Id == updateEventGalleryDto.Id);
            if (eventGallery == null)
                return Result.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = ResponseMessage.EventNotFound}));

            eventGallery.IsConfirmed = updateEventGalleryDto.IsConfirmed;

            if (string.IsNullOrEmpty(updateEventGalleryDto.Image))
                eventGallery.Image = updateEventGalleryDto.Image;

            await Context.SaveChangesAsync();
            return Result.SuccessFull();
        }

        public async Task<Result<EventDto>> Create(CreateEventDto createEventDto, Guid userId)
        {
            var category = await Context.Categories.FirstOrDefaultAsync(c => c.Id == createEventDto.CategoryId);
            if (category == null)
                return Result<EventDto>.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = ResponseMessage.CategoryNotFound}));

            var host = await Context.Hosts.FirstOrDefaultAsync(c => c.Id == createEventDto.HostId);
            if (host == null)
                return Result<EventDto>.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = ResponseMessage.HostNotFound}));

            var user = await Context.Users.FirstOrDefaultAsync(c => c.Id == userId);
            if (user == null)
                return Result<EventDto>.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = ResponseMessage.UserNotFound}));


            var newEvent = _mapper.Map<Event>(createEventDto);

            var dateFromHour = createEventDto.EventSchedule.StartHour;
            var dateToHour = createEventDto.EventSchedule.EndHour;
            TimeSpan fromHour;
            TimeSpan toHour;
            if (TimeSpan.TryParse(dateFromHour, out fromHour))
                if (TimeSpan.TryParse(dateToHour, out toHour))
                {
                    newEvent.EventSchedule.StartHour = fromHour;
                    newEvent.EventSchedule.EndHour = toHour;
                }

            newEvent.EventGallery?.ForEach(e =>
            {
                e.User = user;
                e.Id = Guid.NewGuid();
            });
            await AddAsync(newEvent);
            await Context.SaveChangesAsync();

            return Result<EventDto>.SuccessFull(_mapper.Map<EventDto>(newEvent));
        }

        public async Task<Result> Update(UpdateEventDto updateEventDto, Guid userId)
        {
            var category = await Context.Categories.FirstOrDefaultAsync(c => c.Id == updateEventDto.CategoryId);
            if (category == null)
                return Result.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = ResponseMessage.CategoryNotFound}));

            var host = await Context.Hosts.FirstOrDefaultAsync(c => c.Id == updateEventDto.HostId);
            if (host == null)
                return Result.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = ResponseMessage.HostNotFound}));


            var eventDetail =
                await FirstOrDefaultAsync(x => x.Id == updateEventDto.Id && !x.IsDeleted
                    , e => e.Category,
                    e => e.Host, e => e.UserInEvents.Select(ur => ur.User),
                    e => e.EventGallery.Select(gallery => gallery.User), e => e.EventSchedule,
                    e => e.Comment.Select(c => c.ParentComment), e => e.Comment.Select(c => c.Children));

            if (eventDetail is null)
                return Result.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = ResponseMessage.EventNotFound}));


            if (eventDetail.UserInEvents != null && eventDetail.UserInEvents.Any())
                Context.RemoveRange(eventDetail.UserInEvents);
            if (eventDetail.EventGallery != null && eventDetail.EventGallery.Any())
                Context.RemoveRange(eventDetail.EventGallery);

            eventDetail.UserInEvents = updateEventDto.Users?
                .Select(uId => new UserInEvent {Guid = Guid.NewGuid(), EventId = eventDetail.Id, UserId = uId})
                .ToList();

            eventDetail.EventGallery = updateEventDto.EventGallery?
                .Select(gallery => new EventGallery
                    {Id = Guid.NewGuid(), EventId = eventDetail.Id, UserId = userId, Image = gallery})
                .ToList();

            if (eventDetail.UserInEvents != null && eventDetail.UserInEvents.Any())
                Context.AddRange(eventDetail.UserInEvents);
            if (eventDetail.EventGallery != null && eventDetail.EventGallery.Any())
                Context.AddRange(eventDetail.EventGallery);


            _mapper.Map(updateEventDto, eventDetail);

            var dateFromHour = updateEventDto.EventSchedule.StartHour;
            var dateToHour = updateEventDto.EventSchedule.EndHour;
            TimeSpan fromHour;
            TimeSpan toHour;
            if (TimeSpan.TryParse(dateFromHour, out fromHour))
                if (TimeSpan.TryParse(dateToHour, out toHour))
                {
                    eventDetail.EventSchedule.StartHour = fromHour;
                    eventDetail.EventSchedule.EndHour = toHour;
                }

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
                    {Message = ResponseMessage.EventNotFound}));

            eventDetail.IsDeleted = true;
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }
    }
}