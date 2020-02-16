using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Province;

namespace Hastnama.Ekipchi.Api.Core.Validator.Province
{
    public class CreateProvinceValidator : AbstractValidator<CreateProvinceDto>
    {
        public CreateProvinceValidator()
        {
            RuleFor(dto => dto.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.ProvinceNameIsInvalid);
        }
    }
}