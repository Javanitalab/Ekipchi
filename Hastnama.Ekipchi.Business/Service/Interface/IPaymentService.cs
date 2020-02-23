using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Financial;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IPaymentService : IRepository<Payment>
    {
        Task<Result<PagedList<PaymentDto>>> List(PagingOptions pagingOptions);
        Task<Result<PaymentDto>> Create(PaymentDto dto);
        Task<Result<PaymentDto>> Get(long id);

    }
}