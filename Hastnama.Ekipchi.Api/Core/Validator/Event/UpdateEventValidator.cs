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
                .NotEmpty().WithMessage(ResponseMessage.InvalidCategoryId);

            RuleFor(dto => dto.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidEventName);

            RuleFor(dto => dto.HostId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidHostId);

            RuleFor(dto => dto.EventSchedule)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidEventSchedule)
                .Must(dto =>
                    dto.StartHour != null && dto.EndHour != null && dto.EventDate != null &&
                    dto.RegistrationDate != null && dto.EndRegistrationDate != null)
                .WithMessage(ResponseMessage.InvalidEventSchedule);
        }
    }
}