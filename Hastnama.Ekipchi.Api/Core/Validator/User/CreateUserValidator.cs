using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Auth;
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
                .WithMessage(PersianErrorMessage.InvalidUserCredential);

            RuleFor(dto => dto.Mobile)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.InvalidMobile)
                .MaximumLength(16).WithMessage(PersianErrorMessage.InvalidMobile);
            
            RuleFor(dto => dto.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .EmailAddress().WithMessage(PersianErrorMessage.InvalidEmailAddress)
                .MaximumLength(32).WithMessage(PersianErrorMessage.InvalidEmailAddress);

            RuleFor(dto => dto.Role)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.InvalidUserRole)
                .IsInEnum().WithMessage(PersianErrorMessage.InvalidUserRole);

            RuleFor(dto => dto.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.InvalidNameOrFamily)
                .MaximumLength(16).WithMessage(PersianErrorMessage.InvalidNameOrFamily);

            RuleFor(dto => dto.Family)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.InvalidNameOrFamily)
                .MaximumLength(16).WithMessage(PersianErrorMessage.InvalidNameOrFamily);
        }
    }
}