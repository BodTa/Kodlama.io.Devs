
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Commands.Update;

public class UpdateOperationClaimCommand:IRequest<UpdatedUserOperationClaimDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
}
public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, UpdatedUserOperationClaimDto>
{
    private readonly IMapper _mapper;
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

    public UpdateOperationClaimCommandHandler(IMapper mapper, IOperationClaimRepository operationClaimRepository, OperationClaimBusinessRules operationClaimBusinessRules)
    {
        _mapper = mapper;
        _operationClaimRepository = operationClaimRepository;
        _operationClaimBusinessRules = operationClaimBusinessRules;
    }

    public async Task<UpdatedUserOperationClaimDto> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
    {
        var operationClaim = _mapper.Map<OperationClaim>(request);
        await _operationClaimBusinessRules.OperationClaimMustBeExistWhenRequested(operationClaim);
        OperationClaim updatedClaim = await _operationClaimRepository.UpdateAsync(operationClaim);
        var mappedUpdatedClaim = _mapper.Map<UpdatedUserOperationClaimDto>(updatedClaim);
        return mappedUpdatedClaim;
    }
}
