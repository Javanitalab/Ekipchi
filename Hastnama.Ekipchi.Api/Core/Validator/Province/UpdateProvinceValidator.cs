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

            
        }
    }
}