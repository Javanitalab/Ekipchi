using System;
using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Coupon;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Api.Areas.Admin
{
    public class CouponController : BaseAdminController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CouponController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Coupon List
        /// </summary>
        /// <param name="filterQueryDto"></param>
        /// <param name="pagingOptions"></param>
        /// <returns>Coupon List</returns>
        /// <response code="200">if Get List successfully </response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PagedList<CouponDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] PagingOptions pagingOptions,
            [FromQuery] FilterCouponQueryDto filterQueryDto)
        {
            var result = await _unitOfWork.CouponService.List(pagingOptions, filterQueryDto);
            return result.ApiResult;
        }

        /// <summary>
        /// Coupon Detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Coupon Detail</returns>
        /// <response code="200">if Get successfully </response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(CouponDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}", Name = "GetCoupon")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _unitOfWork.CouponService.Get(id);
            return result.ApiResult;
        }

        /// <summary>
        /// Update Coupon 
        /// </summary>
        /// <param name="updateCouponDto"></param>
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
        public async Task<IActionResult> Update(Guid? id,[FromBody] UpdateCouponDto updateCouponDto)
        {
            
            if (id != null)
                updateCouponDto.Id = id.Value;

            var result = await _unitOfWork.CouponService.Update(updateCouponDto);
            if (!result.Success)
                return result.ApiResult;
            return NoContent();
        }


        /// <summary>
        /// Create Coupon 
        /// </summary>
        /// <param name="createCouponDto"></param>
        /// <returns>NoContent</returns>
        /// <response code="201">if Create successfully </response>
        /// <response code="400">If validation failure.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCouponDto createCouponDto)
        {
            var result = await _unitOfWork.CouponService.Create(createCouponDto);
            if (!result.Success)
                return result.ApiResult;
            return Created(Url.Link("GetCoupon", new {result.Data.Id}), _mapper.Map<CouponDto>(result.Data));
        }
        
        /// <summary>
        /// Delete Coupon 
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
            var result = await _unitOfWork.CouponService.Delete(id);
            if (!result.Success)
                return result.ApiResult;
            return NoContent();
        }

    }
}