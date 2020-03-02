using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.User;

namespace Hastnama.Ekipchi.Api.Core.Validator.User
{
    public class UpdateUserValidator : AbstractValidator<AdminUpdateUserDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(dto => dto)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Must(dto =>
                    (!string.IsNullOrEmpty(dto.Email) && new EmailAddressAttribute().IsValid(dto.Email))
                    || (!string.IsNullOrEmpty(dto.Mobile) && dto.Mobile.Length <= 11 &&
                        Regex.IsMatch(dto.Mobile, "^[0-9 ]+$")))
                .WithMessage(ResponseMessage.InvalidUserCredential);

            RuleFor(dto => dto.Mobile)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidMobile)
                .Must(dto =>
                {
                    try
                    {
                        if (!dto.StartsWith("09"))
                            return false;
                        Convert.ToInt64(dto);
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }).WithMessage(ResponseMessage.InvalidMobile)
                .MaximumLength(16).WithMessage(ResponseMessage.InvalidMobile);

            RuleFor(dto => dto.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .EmailAddress().WithMessage(ResponseMessage.InvalidEmailAddress)
                .MaximumLength(32).WithMessage(ResponseMessage.InvalidEmailAddress);

            RuleFor(dto => dto.Roles)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.InvalidUserRole);

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