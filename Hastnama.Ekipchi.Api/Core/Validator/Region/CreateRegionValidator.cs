using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Region;

namespace Hastnama.Ekipchi.Api.Core.Validator.Region
{
    public class CreateRegionValidator : AbstractValidator<CreateRegionDto>
    {
        public CreateRegionValidator()
        {
            RuleFor(dto => dto.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.RegionNameIsInvalid);

            RuleFor(dto => dto.CityId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(0).WithMessage(ResponseMessage.InvalidCityId);
        }
    }
}