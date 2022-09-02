
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules.ProgrammingLanguageBusinessRules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage
{
    public class CreateProgrammingLanguageCommand:IRequest<CreatedProgrammingLanguageDto>
    {
        public string Name { get; set; }
    }
    public class CreateProgrammingLanguageCommandHandler : IRequestHandler<CreateProgrammingLanguageCommand, CreatedProgrammingLanguageDto>
    {
        private readonly IMapper _mapper;
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
        private readonly ProgrammingLanguageBusinessRules programmingLanguageBusinessRules;


        public CreateProgrammingLanguageCommandHandler(IMapper mapper, IProgrammingLanguageRepository programmingLanguageRepository, ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
        {
            _mapper = mapper;
            _programmingLanguageRepository = programmingLanguageRepository;
            this.programmingLanguageBusinessRules = programmingLanguageBusinessRules;
        }

        public async Task<CreatedProgrammingLanguageDto> Handle(CreateProgrammingLanguageCommand request, CancellationToken cancellationToken)
        {
            await programmingLanguageBusinessRules.ProgramingLanguageNameCannotBeDuplicated(request.Name);
            var programmingLanguage = _mapper.Map<ProgrammingLanguage>(request);
            ProgrammingLanguage createdProgramingLanguage = await _programmingLanguageRepository.AddAsync(programmingLanguage);
            var createdProgrammingLanguageDto = _mapper.Map<CreatedProgrammingLanguageDto>(createdProgramingLanguage);
            return createdProgrammingLanguageDto;
        }
    }
}
