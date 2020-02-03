using System;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Event.Schedule;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IEventScheduleService : IRepository<EventSchedule>
    {
        Task<Result<EventSchedule>> GetScheduleAsync(Guid id);
        Task<Result<EventSchedule>> Update(UpdateEventScheduleDto updateEventSchedule);
    }
}