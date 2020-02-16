using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Event;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using System;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Helper;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IEventService : IRepository<Event>
    {
        Task<Result<PagedList<EventDto>>> List(FilterEventQueryDto filterQueryDto);

        Task<Result<EventDto>> Get(Guid id);

        Task<Result<EventDto>> Create(CreateEventDto createEventDto, Guid userId);

        Task<Result> Update(UpdateEventDto updateEvent, Guid userId);

        Task<Result> Delete(Guid id);
    }
}