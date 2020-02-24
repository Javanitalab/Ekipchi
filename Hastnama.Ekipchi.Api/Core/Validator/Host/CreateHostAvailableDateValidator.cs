using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Host.AvailableDate;

namespace Hastnama.Ekipchi.Api.Core.Validator.Host
{
    public class CreateHostAvailableDateValidator : AbstractValidator<HostAvailableDateDto>
    {
        public CreateHostAvailableDateValidator()
        {
            RuleFor(dto => dto.FromHour)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidHostAvailableDate);

            RuleFor(dto => dto.ToHour)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidHostAvailableDate);

            RuleFor(dto => dto.DateTime)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidHostAvailableDate);
        }

    }
}