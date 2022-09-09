using Application.Features.Teches.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.Features.Teches.Queries.GetListTech;

public class GetListTechQuery:IRequest<GetListTechModel>
{
    public PageRequest PageRequest { get; set; }
}
public class TechGetListQueryHandler : IRequestHandler<GetListTechQuery, GetListTechModel>
{
    private readonly IMapper mapper;
    private readonly ITechRepository techRepository;

    public TechGetListQueryHandler(IMapper mapper, ITechRepository techRepository)
    {
        this.mapper = mapper;
        this.techRepository = techRepository;
    }

    public async Task<GetListTechModel> Handle(GetListTechQuery request, CancellationToken cancellationToken)
    {
        IPaginate<Tech> teches = await techRepository.GetListAsync(include:
            m => m.Include(c => c.Language),
            index: request.PageRequest.Page,
            size: request.PageRequest.PageSize
            );
        var mappedTech = mapper.Map<GetListTechModel>(teches);
        return mappedTech;
    }
}
