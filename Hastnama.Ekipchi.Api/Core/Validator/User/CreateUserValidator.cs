using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.User;

namespace Hastnama.Ekipchi.Api.Core.Validator.User
{
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator()
        {
            RuleFor(dto => dto)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Must(dto => (!string.IsNullOrEmpty(dto.Username) && dto.Username.Length < 16)
                             || (!string.IsNullOrEmpty(dto.Email) && new EmailAddressAttribute().IsValid(dto.Email))
                             || (!string.IsNullOrEmpty(dto.Mobile) && dto.Mobile.Length <= 11 &&
                                 Regex.IsMatch(dto.Mobile, "^[0-9 ]+$")))
                .WithMessage(ResponseMessage.InvalidUserCredential);

            RuleFor(dto => dto.Mobile)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidMobile)
                .MaximumLength(16).WithMessage(ResponseMessage.InvalidMobile);

            RuleFor(dto => dto.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .EmailAddress().WithMessage(ResponseMessage.InvalidEmailAddress)
                .MaximumLength(32).WithMessage(ResponseMessage.InvalidEmailAddress);

            // RuleFor(dto => dto.RoleId)
            // .Cascade(CascadeMode.StopOnFirstFailure)
            // .GreaterThanOrEqualTo(1).WithMessage(PersianErrorMessage.InvalidUserRole);

            RuleFor(dto => dto.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidNameOrFamily)
                .MaximumLength(16).WithMessage(ResponseMessage.InvalidNameOrFamily);

            RuleFor(dto => dto.Family)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidNameOrFamily)
                .MaximumLength(16).WithMessage(ResponseMessage.InvalidNameOrFamily);
        }
    }
}