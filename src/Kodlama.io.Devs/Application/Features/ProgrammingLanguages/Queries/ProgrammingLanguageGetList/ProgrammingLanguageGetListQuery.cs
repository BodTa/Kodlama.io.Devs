
using Application.Features.ProgrammingLanguages.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Queries.ProgrammingLanguageGetList
{
    public class ProgrammingLanguageGetListQuery: IRequest<ProgrammingLanguageGetListModel>
    {
        public PageRequest PageRequest { get; set; }
        public class ProgramingLanguageGetListHandle : IRequestHandler<ProgrammingLanguageGetListQuery, ProgrammingLanguageGetListModel>
        {
            private readonly IMapper _mapper;
            private readonly IProgrammingLanguageRepository _programingLanguageRepository;

            public ProgramingLanguageGetListHandle(IMapper mapper, IProgrammingLanguageRepository programingLanguageRepository)
            {
                _mapper = mapper;
                _programingLanguageRepository = programingLanguageRepository;
            }

            public async Task<ProgrammingLanguageGetListModel> Handle(ProgrammingLanguageGetListQuery request, CancellationToken cancellationToken)
            {
                IPaginate<ProgrammingLanguage> ProgramingLanguages = await _programingLanguageRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                var programingLanguageGetListModel = _mapper.Map<ProgrammingLanguageGetListModel>(ProgramingLanguages);
                return programingLanguageGetListModel;
            }
        }
    }
}
