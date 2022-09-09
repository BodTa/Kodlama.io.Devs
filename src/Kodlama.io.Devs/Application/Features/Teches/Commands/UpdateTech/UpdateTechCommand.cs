using Application.Features.Teches.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Teches.Commands.UpdateTech;

public class UpdateTechCommand:IRequest<UpdatedTechDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int LanguageId { get; set; }
}
public class UpdatedTechCommandHanler : IRequestHandler<UpdateTechCommand, UpdatedTechDto>
{
    private readonly IMapper mapper;
    private readonly ITechRepository techRepository;

    public UpdatedTechCommandHanler(IMapper mapper, ITechRepository techRepository
        )
    {
        this.mapper = mapper;
        this.techRepository = techRepository;
    }

    public async Task<UpdatedTechDto> Handle(UpdateTechCommand request, CancellationToken cancellationToken)
    {
        var tech = mapper.Map<Tech>(request);
        Tech updatedTech =await techRepository.UpdateAsync(tech);
        var mappedTech = mapper.Map<UpdatedTechDto>(updatedTech);
        return mappedTech;
    }
}
