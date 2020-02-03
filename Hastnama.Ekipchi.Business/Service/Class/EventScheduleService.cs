using System;
using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Event.Schedule;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class EventScheduleService : Repository<EkipchiDbContext, EventSchedule>, IEventScheduleService
    {
        private readonly IMapper _mapper;
        public EventScheduleService(EkipchiDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<EventSchedule>> GetScheduleAsync(Guid id)
        {
            var eventSchedule = await FirstOrDefaultAsyncAsNoTracking(x => x.EventId == id);

            if (eventSchedule is null)
                return Result<EventSchedule>.Failed(new NotFoundObjectResult(new ApiMessage { Message = PersianErrorMessage.EventNotFound }));

            return Result<EventSchedule>.SuccessFull(_mapper.Map<EventSchedule>(eventSchedule));
        }

        public async Task<Result<EventSchedule>> Update(UpdateEventScheduleDto updateEventSchedule)
        {
            var schedule = await FirstOrDefaultAsync(x => x.EventId == updateEventSchedule.Id);

            if (schedule is null)
                return Result<EventSchedule>.Failed(new NotFoundObjectResult(new ApiMessage { Message = PersianErrorMessage.EventNotFound }));

            _mapper.Map(updateEventSchedule, schedule);
            await Context.SaveChangesAsync();

            return Result<EventSchedule>.SuccessFull(_mapper.Map<EventSchedule>(schedule));
        }
    }
}