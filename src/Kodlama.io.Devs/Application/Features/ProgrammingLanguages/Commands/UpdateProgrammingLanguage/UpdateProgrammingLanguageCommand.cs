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

namespace Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage
{
    public class UpdateProgrammingLanguageCommand : IRequest<UpdatedProgrammingLanguageDto>
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
    public class UpdatedProgrammingLanguageCommandHanler : IRequestHandler<UpdateProgrammingLanguageCommand, UpdatedProgrammingLanguageDto>
    {
        private readonly IMapper _mapper;
        private readonly IProgrammingLanguageRepository programmingLanguageRepository;
        private readonly ProgrammingLanguageBusinessRules programmingLanguageBusinessRules;

        public UpdatedProgrammingLanguageCommandHanler(IMapper mapper, IProgrammingLanguageRepository programmingLanguageRepository, ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
        {
            _mapper = mapper;
            this.programmingLanguageRepository = programmingLanguageRepository;
            this.programmingLanguageBusinessRules = programmingLanguageBusinessRules;
        }

        public async Task<UpdatedProgrammingLanguageDto> Handle(UpdateProgrammingLanguageCommand request, CancellationToken cancellationToken)
        {
            var programmingLaguage = _mapper.Map<ProgrammingLanguage>(request);
            await programmingLanguageBusinessRules.ProgramingLanguageShouldExistWhenRequested(programmingLaguage);
            ProgrammingLanguage ProgrammingLanguage = await programmingLanguageRepository.UpdateAsync(programmingLaguage);
            var updatedProgrammingLanguage = _mapper.Map<UpdatedProgrammingLanguageDto>(ProgrammingLanguage);
            return updatedProgrammingLanguage;
        } 
    }
}
