using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.BlogCategory;

namespace Hastnama.Ekipchi.Api.Core.Validator.BlogCategory
{
    public class CreateBlogCategoryValidator : AbstractValidator<CreateBlogCategoryDto>
    {
        public CreateBlogCategoryValidator()
        {
            RuleFor(dto => dto.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.DuplicateBlogCategoryName);

            RuleFor(dto => dto.Slug)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidBlogCategorySlug);
        }
    }
}