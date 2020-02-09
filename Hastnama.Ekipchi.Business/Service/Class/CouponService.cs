using System;
using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Coupon;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class CouponService : Repository<EkipchiDbContext, Coupon>, ICouponService
    {
        private readonly IMapper _mapper;

        public CouponService(EkipchiDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<PagedList<CouponDto>>> List(FilterCouponQueryDto filterQueryDto)
        {
            var counties = await WhereAsyncAsNoTracking(c =>
                    (filterQueryDto.IsActive == null || c.IsActive == filterQueryDto.IsActive)
                    && (filterQueryDto.Amount == null || c.Amount < filterQueryDto.Amount)
                    && (filterQueryDto.StartDate == null || c.StartDate >= filterQueryDto.StartDate)
                    && (filterQueryDto.EndDate == null || c.EndDate <= filterQueryDto.EndDate),
                filterQueryDto);


            return Result<PagedList<CouponDto>>.SuccessFull(counties.MapTo<CouponDto>(_mapper));
        }

        public async Task<Result> Update(UpdateCouponDto updateCouponDto)
        {

            var coupon = await FirstOrDefaultAsync(c => c.Id == updateCouponDto.Id);
            _mapper.Map(updateCouponDto, coupon);
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }

        public async Task<Result<CouponDto>> Create(CreateCouponDto createCouponDto)
        {
            var coupon = _mapper.Map(createCouponDto, new Coupon());
            await AddAsync(coupon);
            await Context.SaveChangesAsync();

            return Result<CouponDto>.SuccessFull(_mapper.Map<CouponDto>(coupon));
        }

        public async Task<Result<CouponDto>> Get(Guid id)
        {
            var coupon = await FirstOrDefaultAsyncAsNoTracking(c => c.Id == id);
            if (coupon == null)
                return Result<CouponDto>.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = PersianErrorMessage.CouponNotFound}));

            return Result<CouponDto>.SuccessFull(_mapper.Map<CouponDto>(coupon));
        }
        
        public async Task<Result> Delete(Guid id)
        {
            var coupon = await FirstOrDefaultAsyncAsNoTracking(c => c.Id == id);
            if (coupon == null)
                return Result.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = PersianErrorMessage.CouponNotFound}));

            Delete(coupon);
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }

    }
}