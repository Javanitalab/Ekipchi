using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Data.Event.Gallery;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Business.Service;
using Hastnama.Ekipchi.Common.Message;

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

        /// <summary>
        /// Event Gallery
        /// </summary>
        /// <param name="id"></param>
        /// <returns>EventGallery</returns>
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
            var gallery = await _unitOfWork.EventGalleryService.GetAllAsync(id);
            return gallery.ApiResult;
        }

        /// <summary>
        /// Update Event Gallery
        /// </summary>
        /// <param name="updateEventGallery"></param>
        /// <returns>NoContent</returns>
        /// <response code="204">if updated successfully </response>
        /// <response code="400">If validation failure.</response>
        /// <response code="404">If item not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
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