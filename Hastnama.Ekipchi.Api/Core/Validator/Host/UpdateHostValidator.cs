using System.Linq;
using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Host;

namespace Hastnama.Ekipchi.Api.Core.Validator.Host
{
    public class UpdateHostValidator : AbstractValidator<UpdateHostDto>
    {
        public UpdateHostValidator()
        {
            RuleFor(dto => dto.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidHostName);

            RuleFor(dto => dto.Categories)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidCategoryId);

            RuleFor(dto => dto.HostAvailableDates)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidHostAvailableDate)
                .Must(dto => dto.All(d => new CreateHostAvailableDateValidator().Validate(d).IsValid)).WithMessage(ResponseMessage.InvalidHostAvailableDate);
        }
    }
}