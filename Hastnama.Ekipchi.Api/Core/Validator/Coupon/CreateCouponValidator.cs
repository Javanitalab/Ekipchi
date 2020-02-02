using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Category;
using Hastnama.Ekipchi.Data.Comment;
using Hastnama.Ekipchi.Data.Coupon;
using Hastnama.Ekipchi.Data.Faq;

namespace Hastnama.Ekipchi.Api.Core.Validator.City
{
    public class CreateCouponValidator : AbstractValidator<CreateCouponDto>
    {
        public CreateCouponValidator()
        {
            RuleFor(dto => dto.Code)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.InvalidCouponCode);

        }
    }
}