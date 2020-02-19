using System;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Financial;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IFinancialTransactionService : IRepository<FinancialTransaction>
    {
        Task<Result<PagedList<FinancialTransactionDto>>> List(FilterFinancialTransactionQueryDto filterQueryDto);
        Task<Result<FinancialTransactionDto>> Get(Guid id);
    }
}