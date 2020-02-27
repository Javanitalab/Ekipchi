using System;
using FluentValidation;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Group;

namespace Hastnama.Ekipchi.Api.Core.Validator.Group
{
    public class UpdateGroupValidator : AbstractValidator<UpdateGroupDto>
    {
        public UpdateGroupValidator()
        {
            RuleFor(dto => dto.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ResponseMessage.GroupNameIsInvalid);

            RuleFor(dto => dto.OwnerId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEqual(Guid.Empty).WithMessage(ResponseMessage.GroupOwnerIsInvalid);
        }
    }
}