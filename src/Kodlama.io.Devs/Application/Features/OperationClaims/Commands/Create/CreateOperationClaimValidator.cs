using Core.Security.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Commands.Create
{
    public class CreateOperationClaimValidator:AbstractValidator<OperationClaim>
    {
        public CreateOperationClaimValidator()
        {
            RuleFor(c => c.Name).MinimumLength(1);
        }
    }
}
