using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Blog;

namespace Hastnama.Ekipchi.Api.Core.Validator.Blog
{
    public class CreateBlogValidator : AbstractValidator<CreateBlogDto>
    {
        public CreateBlogValidator()
        {
            RuleFor(dto => dto.Title)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.BlogTitleIsInvalid);

            RuleFor(dto => dto.UserId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.InvalidUserId);

            RuleFor(dto => dto.BlogCategoryId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1).WithMessage(PersianErrorMessage.InvalidBlogCategoryId);
        }
    }
}