using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Financial;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class PaymentService : Repository<EkipchiDbContext, Payment>, IPaymentService
    {
        private readonly IMapper _mapper;

        public PaymentService(EkipchiDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<PagedList<PaymentDto>>> List(PagingOptions pagingOptions)
        {
            var payments = await WhereAsyncAsNoTracking(c => true,
                pagingOptions);

            return Result<PagedList<PaymentDto>>.SuccessFull(payments.MapTo<PaymentDto>(_mapper));
        }

        public async Task<Result<PaymentDto>> Create(PaymentDto dto)
        {
            var payment = _mapper.Map<Payment>(dto);
            Add(payment);
            await Context.SaveChangesAsync();

            return Result<PaymentDto>.SuccessFull(_mapper.Map<PaymentDto>(payment));
        }

        public async Task<Result<PaymentDto>> Get(long id)
        {
            var payment = await FirstOrDefaultAsyncAsNoTracking(c => c.Id==id);

            return Result<PaymentDto>.SuccessFull(_mapper.Map<PaymentDto>(payment));
        }
    }
}