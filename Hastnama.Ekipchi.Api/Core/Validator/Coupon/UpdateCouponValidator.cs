using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Coupon;

namespace Hastnama.Ekipchi.Api.Core.Validator.Coupon
{
    public class UpdateCouponValidator : AbstractValidator<UpdateCouponDto>
    {
        public UpdateCouponValidator()
        {
            RuleFor(dto => dto.Code)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.InvalidCouponCode);

            RuleFor(dto => dto.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.InvalidCouponId);
        }
    }
}