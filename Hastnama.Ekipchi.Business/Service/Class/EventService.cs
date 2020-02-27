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


            var newEvent = _mapper.Map<Event>(createEventDto);
            newEvent.EventGallery?.ForEach(e =>
            {
                e.UserId = userId;
                e.Id = Guid.NewGuid();
            });
            newEvent.EventGallery =
                newEvent.EventGallery?.Take(1)
                    .ToList();
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

            eventDetail.UserInEvents = updateEventDto.Users?
                .Select(uId => new UserInEvent {Guid = Guid.NewGuid(), EventId = eventDetail.Id, UserId = uId})
                .ToList();

            if (eventDetail.UserInEvents != null && eventDetail.UserInEvents.Any())
                Context.AddRange(eventDetail.UserInEvents);


            _mapper.Map(updateEventDto, eventDetail);
            eventDetail.Category = category;
            eventDetail.Host = host;
            if (eventDetail.EventGallery != null && eventDetail.EventGallery.Any(e => e.UserId != Guid.Empty))
            {
                eventDetail.EventGallery =
                    eventDetail.EventGallery.Take(1)
                        .ToList();
                eventDetail.EventGallery.Where(e => e.UserId != Guid.Empty).ToList().ForEach(eg => eg.UserId = userId);
            }

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