using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Region;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Api.Areas.Admin
{
    public class RegionController : BaseAdminController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Region List
        /// </summary>
        /// <param name="filterQueryDto"></param>
        /// <param name="pagingOptions"></param>
        /// <returns>Region List</returns>
        /// <response code="200">if login successfully </response>
        /// <response code="400">If validation failure.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PagedList<RegionDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet]
        public async Task<IActionResult> List(
            [FromQuery] FilterRegionQueryDto filterQueryDto)
        {
            var result = await _unitOfWork.RegionService.List(filterQueryDto);
            if (filterQueryDto.Page == null && filterQueryDto.Limit == null)
                return Ok(result.Data.Items);
            return result.ApiResult;
        }

        /// <summary>
        /// Region Detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Region Detail</returns>
        /// <response code="200">if login successfully </response>
        /// <response code="400">If validation failure.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(RegionDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}", Name = "GetRegion")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _unitOfWork.RegionService.Get(id);
            return result.ApiResult;
        }

        /// <summary>
        /// Update Region 
        /// </summary>
        /// <param name="updateRegionDto"></param>
        /// <returns>NoContent</returns>
        /// <response code="200">if login successfully </response>
        /// <response code="400">If validation failure.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            updateRegionDto.Id = id;
            var result = await _unitOfWork.RegionService.Update(updateRegionDto);

            if (!result.Success)
                return result.ApiResult;
            return NoContent();
        }


        /// <summary>
        /// Create Region 
        /// </summary>
        /// <param name="createRegionDto"></param>
        /// <returns>NoContent</returns>
        /// <response code="200">if login successfully </response>
        /// <response code="400">If validation failure.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRegionDto createRegionDto)
        {
            var result = await _unitOfWork.RegionService.Create(createRegionDto);
            if (!result.Success)
                return result.ApiResult;
            return Created(Url.Link("GetRegion", new {result.Data.Id}), _mapper.Map<RegionDto>(result.Data));
        }
    }
}