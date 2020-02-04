using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Auth;

namespace Hastnama.Ekipchi.Api.Core.Validator.Auth
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(dto => dto.Username)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage(PersianErrorMessage.InvalidUserCredential);

            RuleFor(dto => dto.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.InvalidUserCredential)
                .MaximumLength(16).WithMessage(PersianErrorMessage.InvalidUserCredential);
        }
    }
}