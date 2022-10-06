using Application.Features.Teches.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Teches.Commands.DeleteTech;

public class DeleteTechCommand:IRequest<DeletedTechDto>,ISecuredRequest
{
    public int Id { get; set; }
    public string[] Roles { get; } = { "user" };

}
public class DeleteTechCommandHanler : IRequestHandler<DeleteTechCommand, DeletedTechDto>
{
    private readonly IMapper mapper;
    private readonly ITechRepository techRepository;

    public DeleteTechCommandHanler(IMapper mapper, ITechRepository techRepository)
    {
        this.mapper = mapper;
        this.techRepository = techRepository;
    }

    public async Task<DeletedTechDto> Handle(DeleteTechCommand request, CancellationToken cancellationToken)
    {
        var tech = mapper.Map<Tech>(request);
        Tech deletedTech = await techRepository.DeleteAsync(tech);
        var mappedTech = mapper.Map<DeletedTechDto>(deletedTech);
        return mappedTech;
    }
}
