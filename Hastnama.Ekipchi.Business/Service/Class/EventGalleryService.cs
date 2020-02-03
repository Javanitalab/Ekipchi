using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Event.Gallery;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class EventGalleryService : Repository<EkipchiDbContext, EventGallery>, IEventGalleryService
    {
        private readonly IMapper _mapper;
        public EventGalleryService(EkipchiDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<List<EventGalleryDto>>> GetAllAsync(Guid id)
        {
            var eventGalley = await GetAll().Where(x => x.EventId == id).ToListAsync();

            if (!eventGalley.Any())
                return Result<List<EventGalleryDto>>.Failed(new NotFoundObjectResult(new ApiMessage { Message = PersianErrorMessage.EventNotFound }));

            return Result<List<EventGalleryDto>>.SuccessFull(_mapper.Map<List<EventGalleryDto>>(eventGalley));
        }

        public async Task<Result<EventGalleryDto>> Update(UpdateEventGalleryDto updateEventGallery)
        {
            var galleryItem = await FirstOrDefaultAsync(x => x.EventId == updateEventGallery.Id);

            if (galleryItem is null)
                return Result<EventGalleryDto>.Failed(new NotFoundObjectResult(new ApiMessage { Message = PersianErrorMessage.EventNotFound }));

            _mapper.Map(updateEventGallery, galleryItem);
            await Context.SaveChangesAsync();

            return Result<EventGalleryDto>.SuccessFull(_mapper.Map<EventGalleryDto>(galleryItem));
        }
    }
}