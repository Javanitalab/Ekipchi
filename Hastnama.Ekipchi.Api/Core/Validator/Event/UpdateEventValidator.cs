using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Event;

namespace Hastnama.Ekipchi.Api.Core.Validator.Event
{
    public class UpdateEventValidator : AbstractValidator<UpdateEventDto>
    {
        public UpdateEventValidator()
        {
            RuleFor(dto => dto.CategoryId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.InvalidCategoryId);

            RuleFor(dto => dto.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.InvalidEventName);

            RuleFor(dto => dto.HostId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.InvalidHostId);

            RuleFor(dto => dto.EventSchedule)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.InvalidEventSchedule);
        }
    }
}