using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Country;

namespace Hastnama.Ekipchi.Api.Core.Validator.County
{
    public class UpdateCountyValidator : AbstractValidator<UpdateCountyDto>
    {
        public UpdateCountyValidator()
        {
            RuleFor(dto => dto.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.CityNameIsInvalid);

            RuleFor(dto => dto.ProvinceId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1).WithMessage(ResponseMessage.InvalidProvinceId);
        }
    }
}