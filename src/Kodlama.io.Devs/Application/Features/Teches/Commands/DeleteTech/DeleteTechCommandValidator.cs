using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Teches.Commands.DeleteTech
{
    public class DeleteTechCommandValidator:AbstractValidator<DeleteTechCommand>
    {
        public DeleteTechCommandValidator()
        {
            RuleFor(t => t.Id).NotEmpty();
        }
    }
}
