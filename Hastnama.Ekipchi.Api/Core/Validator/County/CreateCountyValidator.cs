using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.City;
using Hastnama.Ekipchi.Data.Country;

namespace Hastnama.Ekipchi.Api.Core.Validator.City
{
    public class CreateCountyValidator : AbstractValidator<CreateCountyDto>
    {
        public CreateCountyValidator()
        {
            RuleFor(dto => dto.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.CityNameIsInvalid);

            RuleFor(dto => dto.ProvinceId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1).WithMessage(PersianErrorMessage.InvalidProvinceId);
        }
    }
}