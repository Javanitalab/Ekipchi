using System;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Business.Service;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Financial;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Api.Areas.Admin
{
    public class FinancialTransactionController : BaseAdminController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FinancialTransactionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        
        /// <summary>
        /// FinancialTransaction List
        /// </summary>
        /// <param name="filterQueryDto"></param>
        /// <returns>FinancialTransaction List</returns>
        /// <response code="200">if Get List successfully </response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PagedList<FinancialTransactionDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet]
        public async Task<IActionResult> List(
            [FromQuery] FilterFinancialTransactionQueryDto filterQueryDto)
        {
            var result = await _unitOfWork.FinancialTransactionService.List(filterQueryDto);
            return result.ApiResult;
        }
        
        /// <summary>
        /// FinancialTransaction Detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns>FinancialTransaction Detail</returns>
        /// <response code="200">if Get successfully </response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(FinancialTransactionDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}", Name = "GetFinancialTransaction")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _unitOfWork.FinancialTransactionService.Get(id);
            return result.ApiResult;
        }

    }
}