using System;
using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Comment;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Api.Areas.Admin
{
    public class CommentController : BaseAdminController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Comment List
        /// </summary>
        /// <param name="pagingOptions"></param>
        /// <returns>Comment List</returns>
        /// <response code="200">if Get List successfully </response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PagedList<CommentDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] PagingOptions pagingOptions,[FromQuery]FilterCommentQueryDto filterCommentQueryDto)
        {
            var result = await _unitOfWork.CommentService.List(pagingOptions,filterCommentQueryDto);
            return result.ApiResult;
        }

        /// <summary>
        /// Comment Detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Comment Detail</returns>
        /// <response code="200">if Get successfully </response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(CommentDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}", Name = "GetComment")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _unitOfWork.CommentService.Get(id);
            return result.ApiResult;
        }

        /// <summary>
        /// Update Comment 
        /// </summary>
        /// <param name="updateCommentDto"></param>
        /// <returns>NoContent</returns>
        /// <response code="204">if Update successfully </response>
        /// <response code="400">If validation failure.</response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid? id,[FromBody] UpdateCommentDto updateCommentDto)
        {
            
            if (id != null)
                updateCommentDto.Id = id.Value;

            var result = await _unitOfWork.CommentService.Update(updateCommentDto);
            if (!result.Success)
                return result.ApiResult;
            return NoContent();
        }


        /// <summary>
        /// Create Comment 
        /// </summary>
        /// <param name="createCommentDto"></param>
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
        public async Task<IActionResult> Create([FromBody] CreateCommentDto createCommentDto)
        {
            var result = await _unitOfWork.CommentService.Create(createCommentDto,UserId);
            if (!result.Success)
                return result.ApiResult;
            return Created(Url.Link("GetCounty", new {result.Data.Id}), _mapper.Map<CommentDto>(result.Data));
        }
        
        /// <summary>
        /// Delete Comment 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>NoContent</returns>
        /// <response code="204">if Delete successfully </response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _unitOfWork.CommentService.Delete(id);
            if (!result.Success)
                return result.ApiResult;
            return NoContent();
        }
    }
}