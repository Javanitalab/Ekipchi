using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Category;
using Hastnama.Ekipchi.Data.City;
using Hastnama.Ekipchi.Data.Comment;
using Hastnama.Ekipchi.Data.Faq;

namespace Hastnama.Ekipchi.Api.Core.Validator.City
{
    public class UpdateFaqValidator : AbstractValidator<UpdateFaqDto>
    {
        public UpdateFaqValidator()
        {
            RuleFor(dto => dto.Question)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.InvalidFaqQuestion);

            RuleFor(dto => dto.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1).WithMessage(PersianErrorMessage.InvalidFaqId);
            
        }
    }
}