using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Teches.Commands.UpdateTech;

public class UpdateTechCommandValidator:AbstractValidator<UpdateTechCommand>
{
    public UpdateTechCommandValidator()
    {
        RuleFor(t=>t.Id).NotEmpty();
        RuleFor(t => t.LanguageId).NotEmpty();
        RuleFor(t=> t.Name).NotEmpty(); 
    }
}
