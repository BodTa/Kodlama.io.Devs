

using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.Delete;

public class DeleteUserOperationClaimCommand:IRequest<DeletedUserOperationClaimDto>,ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles { get; } = { "admin" };
}
public class DeleteUserOperationClaimCommandHanlder : IRequestHandler<DeleteUserOperationClaimCommand, DeletedUserOperationClaimDto>
{
    private readonly IMapper _mapper;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly UserOperationClaimBusinessRule _userOperationClaimBusinessRule;

    public DeleteUserOperationClaimCommandHanlder(IMapper mapper, IUserOperationClaimRepository userOperationClaimRepository, UserOperationClaimBusinessRule userOperationClaimBusinessRule)
    {
        _mapper = mapper;
        _userOperationClaimRepository = userOperationClaimRepository;
        _userOperationClaimBusinessRule = userOperationClaimBusinessRule;
    }

    async Task<DeletedUserOperationClaimDto> IRequestHandler<DeleteUserOperationClaimCommand, DeletedUserOperationClaimDto>.Handle(DeleteUserOperationClaimCommand request, CancellationToken cancellationToken)
    {
        var userOperationClaim = _mapper.Map<UserOperationClaim>(request);
        await _userOperationClaimBusinessRule.UserOperationClaimShouldExistWhenRequested(userOperationClaim);
        UserOperationClaim deletedClaim = await _userOperationClaimRepository.DeleteAsync(userOperationClaim);
        var mappedClaim = _mapper.Map<DeletedUserOperationClaimDto>(deletedClaim);
        return mappedClaim;
    }
}
