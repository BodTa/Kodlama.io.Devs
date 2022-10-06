using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Commands.Create;

public class CreateOperationClaimCommand:IRequest<CreatedOperationClaimDto>
{
    public string Name { get; set; }
}

public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, CreatedOperationClaimDto>
{
    private readonly IMapper _mapper;
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

    public CreateOperationClaimCommandHandler(IMapper mapper, IOperationClaimRepository operationClaimRepository, OperationClaimBusinessRules operationClaimBusinessRules)
    {
        _mapper = mapper;
        _operationClaimRepository = operationClaimRepository;
        _operationClaimBusinessRules = operationClaimBusinessRules;
    }

    public async Task<CreatedOperationClaimDto> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
    {
        await _operationClaimBusinessRules.OperationClaimNameCannotBeDuplicated(request.Name);
        var mappedRequest =  _mapper.Map<OperationClaim>(request);
        OperationClaim createdOperationClaim = await _operationClaimRepository.AddAsync(mappedRequest);
        var mappedOperationClaim = _mapper.Map<CreatedOperationClaimDto>(createdOperationClaim);
        return mappedOperationClaim;
    }
}
