using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.City;
using Hastnama.Ekipchi.Data.Country;

namespace Hastnama.Ekipchi.Api.Core.Validator.City
{
    public class UpdateCountyValidator : AbstractValidator<UpdateCountyDto>
    {
        public UpdateCountyValidator()
        {
            RuleFor(dto => dto.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.CityNameIsInvalid);

            RuleFor(dto => dto.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1).WithMessage(PersianErrorMessage.InvalidCountyId);
            
            RuleFor(dto => dto.ProvinceId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1).WithMessage(PersianErrorMessage.InvalidProvinceId);
        }
    }
}