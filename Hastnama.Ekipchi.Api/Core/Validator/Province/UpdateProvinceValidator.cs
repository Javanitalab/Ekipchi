using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Province;

namespace Hastnama.Ekipchi.Api.Core.Validator.Province
{
    public class UpdateProvinceValidator : AbstractValidator<UpdateProvinceDto>
    {
        public UpdateProvinceValidator()
        {
            RuleFor(dto => dto.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.ProvinceNameIsInvalid);

            RuleFor(dto => dto.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1).WithMessage(PersianErrorMessage.InvalidProvinceId);
            
        }
    }
}