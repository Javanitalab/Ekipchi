using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Category;
using Hastnama.Ekipchi.Data.City;
using Hastnama.Ekipchi.Data.Comment;
using Hastnama.Ekipchi.Data.Coupon;
using Hastnama.Ekipchi.Data.Faq;

namespace Hastnama.Ekipchi.Api.Core.Validator.City
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