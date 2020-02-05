using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Group;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Api.Areas.Admin
{
    public class GroupController : BaseAdminController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GroupController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Group List
        /// </summary>
        /// <param name="filterQueryDto"></param>
        /// <param name="pagingOptions"></param>
        /// <returns>Group List</returns>
        /// <response code="200">if Get List successfully </response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PagedList<GroupDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] PagingOptions pagingOptions,
            [FromQuery] FilterGroupQueryDto filterQueryDto)
        {
            var result = await _unitOfWork.GroupService.List(pagingOptions, filterQueryDto);
            return result.ApiResult;
        }

        /// <summary>
        /// Group Detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Group Detail</returns>
        /// <response code="200">if Get successfully </response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(GroupDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}", Name = "GetGroup")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _unitOfWork.GroupService.Get(id);
            return result.ApiResult;
        }

        /// <summary>
        /// Update Group 
        /// </summary>
        /// <param name="updateGroupDto"></param>
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
        public async Task<IActionResult> Update([FromBody] UpdateGroupDto updateGroupDto)
        {
            var result = await _unitOfWork.GroupService.Update(updateGroupDto);
            if (!result.Success)
                return result.ApiResult;
            return NoContent();
        }


        /// <summary>
        /// Create Group 
        /// </summary>
        /// <param name="createGroupDto"></param>
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
        public async Task<IActionResult> Create([FromBody] CreateGroupDto createGroupDto)
        {
            var result = await _unitOfWork.GroupService.Create(createGroupDto);
            if (!result.Success)
                return result.ApiResult;
            return Created(Url.Link("GetGroup", new {result.Data.Id}), _mapper.Map<GroupDto>(result.Data));
        }
        
        /// <summary>
        /// Delete Group 
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
            var result = await _unitOfWork.GroupService.Delete(id);
            if (!result.Success)
                return result.ApiResult;
            return NoContent();
        }

    }
}
