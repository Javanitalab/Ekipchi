using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Blog;
using Hastnama.Ekipchi.Data.City;

namespace Hastnama.Ekipchi.Api.Core.Validator.City
{
    public class CreateBlogValidator : AbstractValidator<CreateBlogDto>
    {
        public CreateBlogValidator()
        {
            RuleFor(dto => dto.Title)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.BlogTitleIsInvalid);

            RuleFor(dto => dto.BlogCategoryId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1).WithMessage(PersianErrorMessage.InvalidBlogCategoryId);
        }
    }
}