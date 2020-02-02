using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Category;
using Hastnama.Ekipchi.Data.Comment;
using Hastnama.Ekipchi.Data.Faq;

namespace Hastnama.Ekipchi.Api.Core.Validator.City
{
    public class CreateFaqValidator : AbstractValidator<CreateFaqDto>
    {
        public CreateFaqValidator()
        {
            RuleFor(dto => dto.Question)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.InvalidFaqQuestion);

        }
    }
}