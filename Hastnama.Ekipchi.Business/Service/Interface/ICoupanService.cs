using System;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Coupon;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;


namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface ICouponService : IRepository<Coupon>
    {
        Task<Result<PagedList<CouponDto>>> List(PagingOptions pagingOptions, FilterCouponQueryDto filterQueryDto);
        Task<Result> Update(UpdateCouponDto updateCouponDto);
        Task<Result<CouponDto>> Create(CreateCouponDto dto);
        Task<Result<CouponDto>> Get(Guid id);
        Task<Result> Delete(Guid id);

    }
}