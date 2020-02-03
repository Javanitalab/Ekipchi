using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Data.Event.Gallery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Api.Areas.Admin
{
    [Route("[controller]")]
    [ApiController]
    public class EventGalleryController : BaseAdminController
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventGalleryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var gallery = await _unitOfWork.EventGalleryService.GetAllAsync(id);

            if (!gallery.Success)
                return gallery.ApiResult;

            return gallery.ApiResult;
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateEventGalleryDto updateEventGallery)
        {
            var gallery = await _unitOfWork.EventGalleryService.Update(updateEventGallery);

            if (!gallery.Success)
                return NotFound();

            return NoContent();
        }
    }
}