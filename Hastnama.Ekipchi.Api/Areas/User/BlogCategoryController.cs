using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Api.Areas.Admin;
using Hastnama.Ekipchi.Business.Service;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.BlogCategory;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Api.Areas.User
{
    public class BlogCategoryController : BaseUserController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BlogCategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// BlogCategory List
        /// </summary>
        /// <param name="filterQueryDto"></param>
        /// <returns>BlogCategory List</returns>
        /// <response code="200">if Get List successfully </response>
        /// <response code="400">if Param Validation Failure </response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PagedList<BlogCategoryDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] FilterBlogCategoryQueryDto filterQueryDto)
        {
            var result = await _unitOfWork.BlogCategoryService.List(filterQueryDto);
            if (filterQueryDto.Page == null && filterQueryDto.Limit == null)
                return Ok(result.Data.Items);
            return result.ApiResult;
        }

        /// <summary>
        /// BlogCategory Detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BlogCategory Detail</returns>
        /// <response code="200">if Get successfully </response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(BlogCategoryDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _unitOfWork.BlogCategoryService.Get(id);
            return result.ApiResult;
        }
    }
}