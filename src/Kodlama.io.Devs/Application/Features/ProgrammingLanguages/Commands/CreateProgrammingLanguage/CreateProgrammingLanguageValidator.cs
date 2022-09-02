using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage
{
    internal class CreateProgrammingLanguageValidator:AbstractValidator<CreateProgrammingLanguageCommand>
    {
        public CreateProgrammingLanguageValidator()
        {
            RuleFor(c=> c.Name).NotEmpty();
        }
    }
}
