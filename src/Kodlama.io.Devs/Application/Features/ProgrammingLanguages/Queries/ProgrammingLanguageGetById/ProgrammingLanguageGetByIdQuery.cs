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

namespace Application.Features.ProgrammingLanguages.Queries.ProgrammingLanguageGetById
{
    public class ProgrammingLanguageGetByIdQuery : IRequest<ProgrammingLanguageGetByIdDto>
    {
        public int Id { get; set; }
    }

    public class ProgamingLanguageGetByIdQueryHandle : IRequestHandler<ProgrammingLanguageGetByIdQuery, ProgrammingLanguageGetByIdDto>
    {
        private readonly IMapper _mapper;
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
        private readonly ProgrammingLanguageBusinessRules programmingLanguageBusinessRules;

        public ProgamingLanguageGetByIdQueryHandle(IMapper mapper, IProgrammingLanguageRepository programmingLanguageRepository, ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
        {
            _mapper = mapper;
            _programmingLanguageRepository = programmingLanguageRepository;
            this.programmingLanguageBusinessRules = programmingLanguageBusinessRules;
        }

        public async Task<ProgrammingLanguageGetByIdDto> Handle(ProgrammingLanguageGetByIdQuery request, CancellationToken cancellationToken)
        {
            var programmingLanguage = await _programmingLanguageRepository.GetAsync(p=>p.Id==request.Id);
            await programmingLanguageBusinessRules.ProgramingLanguageShouldExistWhenRequested(programmingLanguage);
            var programmingLanguageGetByIdDto = _mapper.Map<ProgrammingLanguageGetByIdDto>(programmingLanguage);
            return programmingLanguageGetByIdDto;
         }
    }
}
