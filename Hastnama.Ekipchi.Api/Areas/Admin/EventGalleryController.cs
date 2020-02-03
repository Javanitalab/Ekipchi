using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Data.Event.Gallery;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
            return gallery.ApiResult;
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateEventGalleryDto updateEventGallery)
        {
            var result = await _unitOfWork.EventGalleryService.Update(updateEventGallery);

            if (!result.Success)
                return result.ApiResult;

            return NoContent();
        }
    }
}