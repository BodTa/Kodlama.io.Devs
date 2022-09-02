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

namespace Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage
{
    public class DeleteProgrammingLanguageCommand:IRequest<DeletedProgrammingLanguageDto>
    {
        public int Id { get; set; }
    }
    public class DeleteProgrammingLanguageCommandHandler : IRequestHandler<DeleteProgrammingLanguageCommand, DeletedProgrammingLanguageDto>
    {
        private readonly IMapper mapper;
        private readonly IProgrammingLanguageRepository programmingLanguageRepository;
        private readonly ProgrammingLanguageBusinessRules programingLanguageBusinessRules;

        public DeleteProgrammingLanguageCommandHandler(IMapper mapper, IProgrammingLanguageRepository programmingLanguageRepository,ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
        {
            this.mapper = mapper;
            this.programmingLanguageRepository = programmingLanguageRepository;
            this.programingLanguageBusinessRules = programmingLanguageBusinessRules;
        }

        public async Task<DeletedProgrammingLanguageDto> Handle(DeleteProgrammingLanguageCommand request, CancellationToken cancellationToken)
        {
            var requestToProgrammingLanguage = mapper.Map<ProgrammingLanguage>(request);
            await programingLanguageBusinessRules.ProgramingLanguageShouldExistWhenRequested(requestToProgrammingLanguage);
            ProgrammingLanguage programmingLanguage = await programmingLanguageRepository.DeleteAsync(requestToProgrammingLanguage);
            var deletedProgrammingLanguage = mapper.Map<DeletedProgrammingLanguageDto>(programmingLanguage);
            return deletedProgrammingLanguage;
        }
    }
}
