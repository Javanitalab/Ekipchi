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
                .NotEmpty().WithMessage(PersianErrorMessage.GroupNameIsInvalid);
            
            RuleFor(dto => dto.UsersInGroups)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(PersianErrorMessage.GroupOwnerIsInvalid);
            
        }
    }
}