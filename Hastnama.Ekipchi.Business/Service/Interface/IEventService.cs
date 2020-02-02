using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Event;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Hastnama.GuitarIranShop.DataAccess.Helper;
using System;
using System.Threading.Tasks;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IEventService : IRepository<Event>
    {
        Task<Result<PagedList<EventDto>>> List(PagingOptions pagingOptions,
            FilterEventQueryDto filterQueryDto);

        Task<Result<EventDto>> Get(Guid id);

        Task<Result<EventDto>> Create(CreateEventDto createEventDto);

        Task<Result> Update(UpdateEventDto updateEvent);

        Task<Result> Delete(Guid id);
    }
}