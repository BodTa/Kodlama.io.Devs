using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;


namespace Application.Features.OperationClaims.Commands.Delete;

public class DeleteOperationClaimCommand:IRequest<DeletedOperationClaimDto>
{
    public int Id { get; set; }
}
public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, DeletedOperationClaimDto>,ISecuredRequest
{
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly IMapper _mapper;
    private readonly OperationClaimBusinessRules _operationClaimBusinessRules;
    public string[] Roles { get; } = { "admin" };


    public DeleteOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, OperationClaimBusinessRules operationClaimBusinessRules)
    {
        _operationClaimRepository = operationClaimRepository;
        _mapper = mapper;
        _operationClaimBusinessRules = operationClaimBusinessRules;
    }

    public async Task<DeletedOperationClaimDto> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
    {
        var operationClaim = _mapper.Map<OperationClaim>(request);
        await _operationClaimBusinessRules.OperationClaimMustBeExistWhenRequested(operationClaim);
        OperationClaim deletedOperationClaim = await _operationClaimRepository.DeleteAsync(operationClaim);
        var mappedDeletedOperationClaim =  _mapper.Map<DeletedOperationClaimDto>(deletedOperationClaim);
        return mappedDeletedOperationClaim;
    }
}
