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
                .NotEmpty().WithMessage(PersianErrorMessage.InvalidHostName);

            RuleFor(dto => dto.Categories)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.InvalidCategoryId);

            RuleFor(dto => dto.HostAvailableDates)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.InvalidHostAvailableDates);
        }
    }
}