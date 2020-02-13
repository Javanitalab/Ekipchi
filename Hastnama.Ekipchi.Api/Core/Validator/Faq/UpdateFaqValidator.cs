using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Faq;

namespace Hastnama.Ekipchi.Api.Core.Validator.Faq
{
    public class UpdateFaqValidator : AbstractValidator<UpdateFaqDto>
    {
        public UpdateFaqValidator()
        {
            RuleFor(dto => dto.Question)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidFaqQuestion);

        }
    }
}