using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.City;
using Hastnama.Ekipchi.Data.Country;
using Hastnama.Ekipchi.Data.Province;

namespace Hastnama.Ekipchi.Api.Core.Validator.City
{
    public class CreateProvinceValidator : AbstractValidator<CreateProvinceDto>
    {
        public CreateProvinceValidator()
        {
            RuleFor(dto => dto.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.ProvinceNameIsInvalid);

        }
    }
}