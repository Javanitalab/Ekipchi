using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Data.Event.Schedule;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Message;

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

        /// <summary>
        /// Event Schedule
        /// </summary>
        /// <param name="id"></param>
        /// <returns>EventSchedule</returns>
        /// <response code="200">if item found </response>
        /// <response code="400">If validation failure.</response>
        /// <response code="404">If item not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var schedule = await _unitOfWork.EventScheduleService.GetScheduleAsync(id);
            return schedule.ApiResult;
        }

        /// <summary>
        /// Update Event Schedule
        /// </summary>
        /// <param name="updateEventSchedule"></param>
        /// <returns>NoContent</returns>
        /// <response code="204">if updated successfully </response>
        /// <response code="400">If validation failure.</response>
        /// <response code="404">If item not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
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