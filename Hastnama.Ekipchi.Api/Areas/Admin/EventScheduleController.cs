using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Business.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}