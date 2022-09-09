using Application.Features.Teches.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Teches.Commands.CreateTech;

public class CreateTechCommand:IRequest<CreatedTechDto>
{
    public string Name { get; set; }
    public int LanguageId { get; set; }
}
public class CreatedTechCommandHandler : IRequestHandler<CreateTechCommand, CreatedTechDto>
{
    private readonly IMapper mapper;
    private readonly ITechRepository techRepository;

    public CreatedTechCommandHandler(IMapper mapper, ITechRepository techRepository)
    {
        this.mapper = mapper;
        this.techRepository = techRepository;
    }

    async Task<CreatedTechDto> IRequestHandler<CreateTechCommand, CreatedTechDto>.Handle(CreateTechCommand request, CancellationToken cancellationToken)
    {
        var newTech = mapper.Map<Tech>(request);
        Tech addedTech = await techRepository.AddAsync(newTech);
        var mappedTech = mapper.Map<CreatedTechDto>(addedTech);
        return mappedTech;
    }
}
