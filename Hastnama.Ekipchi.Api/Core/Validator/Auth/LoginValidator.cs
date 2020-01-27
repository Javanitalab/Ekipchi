using FluentValidation;
using Hastnama.Ekipchi.Api.Core.ApiContent;
using Hastnama.Ekipchi.Data.Auth;

namespace Hastnama.Ekipchi.Api.Core.Validator.Auth
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {

        public LoginValidator()
        {

            RuleFor(dto => dto.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(Response.GetMessageTemplate("PasswordIsInvalid"));
            RuleFor(dto => dto.Username)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage(Response.GetMessageTemplate("UsernameIsInvalid"));
        }


    }
}