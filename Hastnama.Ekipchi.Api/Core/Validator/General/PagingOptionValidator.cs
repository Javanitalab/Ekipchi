using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using FluentValidation;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Auth;

namespace Hastnama.Ekipchi.Api.Core.Validator.General
{
    public class PagingOptionValidator : AbstractValidator<PagingOptions>
    {
        public PagingOptionValidator()
        {
            RuleFor(dto => dto.Page)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .LessThanOrEqualTo(500).WithMessage(ResponseMessage.InvalidPagingOption)
                .GreaterThanOrEqualTo(0).WithMessage(ResponseMessage.InvalidPagingOption);

            RuleFor(dto => dto.Limit)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .LessThanOrEqualTo(100).WithMessage(ResponseMessage.InvalidPagingOption)
                .GreaterThanOrEqualTo(1).WithMessage(ResponseMessage.InvalidPagingOption);

        }
    }
}