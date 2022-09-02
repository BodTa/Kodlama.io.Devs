using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Rules.ProgrammingLanguageBusinessRules
{
    public class ProgrammingLanguageBusinessRules
    {
        private readonly IProgrammingLanguageRepository programingLanguageRepository;

        public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository programingLanguageRepository)
        {
            this.programingLanguageRepository = programingLanguageRepository;
        }

        public async Task ProgramingLanguageNameCannotBeDuplicated(string name)
        {
            IPaginate<ProgrammingLanguage> result= await programingLanguageRepository.GetListAsync(p=>p.Name == name);
            if (result.Items.Any()) throw new BusinessException("Programing Language name cannot be duplicated.");

        }

        public async Task ProgramingLanguageShouldExistWhenRequested(ProgrammingLanguage programingLanguage)
        {
            if (programingLanguage == null) throw new BusinessException("Programind Language does not exist.");
        }
    }
}
