using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Event;

namespace Hastnama.Ekipchi.Api.Core.Validator.Event
{
    public class CreateEventValidator : AbstractValidator<CreateEventDto>
    {
        public CreateEventValidator()
        {
            RuleFor(dto => dto.CategoryId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidCouponCode);

            RuleFor(dto => dto.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidCouponCode);

            RuleFor(dto => dto.HostId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidCouponId);

            RuleFor(dto => dto.EventSchedule)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidCouponId);
        }
    }
}