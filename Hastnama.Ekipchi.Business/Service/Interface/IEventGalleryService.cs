using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Event.Gallery;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IEventGalleryService : IRepository<EventGallery>
    {
        Task<Result<List<EventGalleryDto>>> GetAllAsync(Guid id);
        Task<Result<EventGalleryDto>> Update(UpdateEventGalleryDto updateEventGallery);
    }
}