using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Teches.Commands.CreateTech
{
    public class CreateTechCommandValidator:AbstractValidator<CreateTechCommand>
    {
        public CreateTechCommandValidator()
        {
            RuleFor(t => t.Name).MinimumLength(1);
            RuleFor(t => t.LanguageId).NotEmpty();
        }
    }
}
