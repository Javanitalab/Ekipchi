using System;
using System.Linq;
using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Host;
using Hastnama.Ekipchi.Data.Host.AvailableDate;

namespace Hastnama.Ekipchi.Api.Core.Validator.Host
{
    public class CreateHostValidator : AbstractValidator<CreateHostDto>
    {
        public CreateHostValidator()
        {
            RuleFor(dto => dto.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidHostName);

            RuleFor(dto => dto.Categories)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidCategoryId);

            RuleFor(dto => dto.Capacity)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1).WithMessage(ResponseMessage.InvalidHostCapacity);

            RuleFor(dto => dto.HostAvailableDates)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidHostAvailableDate)
                .Must(dto => dto.All(ValidateEventSchedule))
                .WithMessage(ResponseMessage.InvalidHostAvailableDate);
        }

        private bool ValidateEventSchedule(HostAvailableDateDto dto)
        {
            var dateFromHour = dto.FromHour;
            var dateToHour = dto.ToHour;
            TimeSpan fromHour;
            TimeSpan toHour;
            if (TimeSpan.TryParse(dateFromHour, out fromHour))
                if (TimeSpan.TryParse(dateToHour, out toHour))
                    return true;

            return false;
        }
    }
}