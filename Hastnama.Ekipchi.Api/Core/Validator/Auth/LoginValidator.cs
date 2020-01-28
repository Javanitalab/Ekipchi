using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Auth;

namespace Hastnama.Ekipchi.Api.Core.Validator.Auth
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {

        public LoginValidator()
        {

            RuleFor(dto => dto.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.PasswordIsInvalid);
            RuleFor(dto => dto.Username)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage(PersianErrorMessage.UsernameIsInvalid);
        }


    }
}