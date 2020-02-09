using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Province;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Api.Areas.Admin
{
    public class ProvinceController : BaseAdminController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProvinceController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Province List
        /// </summary>
        /// <param name="filterQueryDto"></param>
        /// <param name="pagingOptions"></param>
        /// <returns>Province List</returns>
        /// <response code="200">if Get List successfully </response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PagedList<ProvinceDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet]
        public async Task<IActionResult> List(
            [FromQuery] FilterProvinceQueryDto filterQueryDto)
        {
            var result = await _unitOfWork.ProvinceService.List( filterQueryDto);
            if (filterQueryDto.Page == null && filterQueryDto.Limit == null)
                return Ok(result.Data.Items);
            return result.ApiResult;
        }

        /// <summary>
        /// Province Detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Province Detail</returns>
        /// <response code="200">if Get successfully </response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ProvinceDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}", Name = "GetProvince")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _unitOfWork.ProvinceService.Get(id);
            return result.ApiResult;
        }

        /// <summary>
        /// Update Province 
        /// </summary>
        /// <param name="updateProvinceDto"></param>
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
        public async Task<IActionResult> Update(int? id,[FromBody] UpdateProvinceDto updateProvinceDto)
        {
            if (id != null)
                updateProvinceDto.Id = id.Value;

            var result = await _unitOfWork.ProvinceService.Update(updateProvinceDto);
            if (!result.Success)
                return result.ApiResult;
            return NoContent();
        }


        /// <summary>
        /// Create Province 
        /// </summary>
        /// <param name="createProvinceDto"></param>
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
        public async Task<IActionResult> Create([FromBody] CreateProvinceDto createProvinceDto)
        {
            var result = await _unitOfWork.ProvinceService.Create(createProvinceDto);
            if (!result.Success)
                return result.ApiResult;
            return Created(Url.Link("GetProvince", new {result.Data.Id}), _mapper.Map<ProvinceDto>(result.Data));
        }
    }
}
