using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Country;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Api.Areas.Admin
{
    public class CountyController : BaseAdminController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountyController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// County List
        /// </summary>
        /// <param name="filterQueryDto"></param>
        /// <param name="pagingOptions"></param>
        /// <returns>County List</returns>
        /// <response code="200">if Get List successfully </response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PagedList<CountyDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] PagingOptions pagingOptions,
            [FromQuery] FilterCountyQueryDto filterQueryDto)
        {
            var result = await _unitOfWork.CountyService.List(pagingOptions, filterQueryDto);
         
            return result.ApiResult;
        }

        /// <summary>
        /// County Detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns>County Detail</returns>
        /// <response code="200">if Get successfully </response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(CountyDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}", Name = "GetCounty")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _unitOfWork.CountyService.Get(id);
         
            return result.ApiResult;
        }

        /// <summary>
        /// Update County 
        /// </summary>
        /// <param name="updateCountyDto"></param>
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
        public async Task<IActionResult> Update([FromBody] UpdateCountyDto updateCountyDto)
        {
            var result = await _unitOfWork.CountyService.Update(updateCountyDto);
          
            if (!result.Success)
                return result.ApiResult;
           
            return NoContent();
        }


        /// <summary>
        /// Create County 
        /// </summary>
        /// <param name="createCountyDto"></param>
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
        public async Task<IActionResult> Create([FromBody] CreateCountyDto createCountyDto)
        {
            var result = await _unitOfWork.CountyService.Create(createCountyDto);
           
            if (!result.Success)
                return result.ApiResult;
           
            return Created(Url.Link("GetCounty", new {result.Data.Id}), _mapper.Map<CountyDto>(result.Data));
        }
    }
}