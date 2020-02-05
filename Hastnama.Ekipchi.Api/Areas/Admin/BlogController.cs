using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Blog;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Api.Areas.Admin
{
    public class BlogController : BaseAdminController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BlogController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Blog List
        /// </summary>
        /// <param name="filterQueryDto"></param>
        /// <param name="pagingOptions"></param>
        /// <returns>Blog List</returns>
        /// <response code="200">if Get successfully </response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PagedList<BlogDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] PagingOptions pagingOptions,
            [FromQuery] FilterBlogQueryDto filterQueryDto)
        {
            var result = await _unitOfWork.BlogService.List(pagingOptions, filterQueryDto);
            return result.ApiResult;
        }

        /// <summary>
        /// Blog Detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Blog Detail</returns>
        /// <response code="200">if Get successfully </response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(BlogDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}", Name = "GetBlog")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _unitOfWork.BlogService.Get(id);
            return result.ApiResult;
        }

        /// <summary>
        /// Update Blog 
        /// </summary>
        /// <param name="updateBlogDto"></param>
        /// <returns>NoContent</returns>
        /// <response code="204">if Update successfully </response>
        /// <response code="400">If validation failure.</response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBlogDto updateBlogDto)
        {
            var result = await _unitOfWork.BlogService.Update(updateBlogDto);
            if (!result.Success)
                return result.ApiResult;
            return NoContent();
        }


        /// <summary>
        /// Create Blog 
        /// </summary>
        /// <param name="createBlogDto"></param>
        /// <returns>NoContent</returns>
        /// <response code="201">if Create successfully </response>
        /// <response code="400">If validation failure.</response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBlogDto createBlogDto)
        {
            var result = await _unitOfWork.BlogService.Create(createBlogDto);
            if (!result.Success)
                return result.ApiResult;
            return Created(Url.Link("GetCounty", new {result.Data.Id}), _mapper.Map<BlogDto>(result.Data));
        }
        
        
        /// <summary>
        /// Delete Blog 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>NoContent</returns>
        /// <response code="204">if Delete successfully </response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.BlogService.Delete(id);
            if (!result.Success)
                return result.ApiResult;
            return NoContent();
        }

    }
}