using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Data.Event.Schedule;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Hastnama.Ekipchi.Api.Areas.Admin
{
    [Route("[controller]")]
    [ApiController]
    public class EventScheduleController : BaseAdminController
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventScheduleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var schedule = await _unitOfWork.EventScheduleService.GetScheduleAsync(id);
            return schedule.ApiResult;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateEventScheduleDto updateEventSchedule)
        {
            var result = await _unitOfWork.EventScheduleService.Update(updateEventSchedule);

            if (!result.Success)
                return result.ApiResult;

            return NoContent();
        }
    }
}