using System;
using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Event;
using Hastnama.Ekipchi.Data.Event.Schedule;

namespace Hastnama.Ekipchi.Api.Core.Validator.Event
{
    public class CreateEventValidator : AbstractValidator<CreateEventDto>
    {
        public CreateEventValidator()
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

            RuleFor(dto => dto)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Must(dto=>dto.MaximumAttendees>=dto.MinimumAttendees).WithMessage(ResponseMessage.InvalidEventAttendees);

            RuleFor(dto => dto.EventSchedule)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidEventSchedule).Must(ValidateEventSchedule)
                .WithMessage(ResponseMessage.InvalidEventSchedule);
        }

        private bool ValidateEventSchedule(EventScheduleDto dto)
        {
            var dateFromHour = dto.StartHour;
            var dateToHour = dto.EndHour;
            TimeSpan fromHour;
            TimeSpan toHour;
            if (TimeSpan.TryParse(dateFromHour, out fromHour))
                if (TimeSpan.TryParse(dateToHour, out toHour))
                    return true;

            return false;
        }
    }
}