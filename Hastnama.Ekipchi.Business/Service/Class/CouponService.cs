using System;
using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Coupon;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Hastnama.GuitarIranShop.DataAccess.Helper;
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

        public async Task<Result<PagedList<CouponDto>>> List(PagingOptions pagingOptions,
            FilterCouponQueryDto filterQueryDto)
        {
            var counties = await WhereAsyncAsNoTracking(c =>
                    (filterQueryDto.IsActive == null || c.IsActive == filterQueryDto.IsActive)
                    && (filterQueryDto.Amount == null || c.Amount < filterQueryDto.Amount)
                    && (filterQueryDto.StartDate == null || c.StartDate >= filterQueryDto.StartDate)
                    && (filterQueryDto.EndDate == null || c.EndDate <= filterQueryDto.EndDate),
                pagingOptions);


            return Result<PagedList<CouponDto>>.SuccessFull(counties.MapTo<CouponDto>(_mapper));
        }

        public async Task<Result> Update(UpdateCouponDto updateCouponDto)
        {
            var duplicateCoupon = await FirstOrDefaultAsyncAsNoTracking(c => c.Code == updateCouponDto.Code);
            if (duplicateCoupon != null)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.DuplicateCouponCode}));

            var Coupon = await FirstOrDefaultAsync(c => c.Id == updateCouponDto.Id);
            _mapper.Map(updateCouponDto, Coupon);
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }

        public async Task<Result<CouponDto>> Create(CreateCouponDto createCouponDto)
        {
            var duplicateCoupon = await FirstOrDefaultAsyncAsNoTracking(c => c.Code == createCouponDto.Code);
            if (duplicateCoupon != null)
                return Result<CouponDto>.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.DuplicateCouponCode}));

            var Coupon = _mapper.Map(createCouponDto, new Coupon());
            await AddAsync(Coupon);
            await Context.SaveChangesAsync();

            return Result<CouponDto>.SuccessFull(_mapper.Map<CouponDto>(Coupon));
        }

        public async Task<Result<CouponDto>> Get(Guid id)
        {
            var Coupon = await FirstOrDefaultAsyncAsNoTracking(c => c.Id == id);
            if (Coupon == null)
                return Result<CouponDto>.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = PersianErrorMessage.CouponNotFound}));

            return Result<CouponDto>.SuccessFull(_mapper.Map<CouponDto>(Coupon));
        }
    }
}