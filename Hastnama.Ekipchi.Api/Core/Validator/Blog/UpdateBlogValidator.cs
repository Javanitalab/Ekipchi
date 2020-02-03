using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Blog;

namespace Hastnama.Ekipchi.Api.Core.Validator.Blog
{
    public class UpdateBlogValidator : AbstractValidator<UpdateBlogDto>
    {
        public UpdateBlogValidator()
        {
            RuleFor(dto => dto.Title)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.BlogTitleIsInvalid);

            RuleFor(dto => dto.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1).WithMessage(PersianErrorMessage.InvalidBlogId);
            
            RuleFor(dto => dto.BlogCategoryId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1).WithMessage(PersianErrorMessage.InvalidBlogCategoryId);
        }
    }
}