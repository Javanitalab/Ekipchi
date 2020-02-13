using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Coupon;

namespace Hastnama.Ekipchi.Api.Core.Validator.Coupon
{
    public class CreateCouponValidator : AbstractValidator<CreateCouponDto>
    {
        public CreateCouponValidator()
        {
            RuleFor(dto => dto.Code)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidCouponCode);

        }
    }
}