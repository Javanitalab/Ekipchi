using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using FluentValidation;
using Hastnama.Ekipchi.Common.Enum;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.User;

namespace Hastnama.Ekipchi.Api.Core.Validator.User
{
    public class UpdateUserStatusValidator : AbstractValidator<AdminUpdateUserStatusDto>
    {
        public UpdateUserStatusValidator()
        {
            RuleFor(dto => dto.Status)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .IsInEnum().WithMessage(ResponseMessage.InvalidUserStatus)
                .NotEqual(UserStatus.Delete).WithMessage(ResponseMessage.InvalidUserStatus);
        }
    }
}