using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.BlogCategory;

namespace Hastnama.Ekipchi.Api.Core.Validator.BlogCategory
{
    public class UpdateBlogCategoryValidator : AbstractValidator<UpdateBlogCategoryDto>
    {
        public UpdateBlogCategoryValidator()
        {
            RuleFor(dto => dto.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.DuplicateBlogCategoryName);
            
            RuleFor(dto => dto.Slug)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.InvalidBlogCategorySlug);
        }
    }
}