using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using MediatR;
namespace Application.Features.UserOperationClaims.Commands.Update;

public class UpdateUserOperationClaimCommand:IRequest<UpdatedUserOperationClaimDto>,ISecuredRequest
{
    public int Id { get; set; }
    public int OperationClaimId { get; set; }
    public int UserId { get; set; }
    public string[] Roles { get; } = { "admin" };

}
public class UpdateUserOperationClaimCommandHandler : IRequestHandler<UpdateUserOperationClaimCommand, UpdatedUserOperationClaimDto>
{
    private readonly IMapper _mapper;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly UserOperationClaimBusinessRule _userOperationClaimBusinessRule;

    public UpdateUserOperationClaimCommandHandler(IMapper mapper, IUserOperationClaimRepository userOperationClaimRepository, UserOperationClaimBusinessRule userOperationClaimBusinessRule)
    {
        _mapper = mapper;
        _userOperationClaimRepository = userOperationClaimRepository;
        _userOperationClaimBusinessRule = userOperationClaimBusinessRule;
    }

    public async Task<UpdatedUserOperationClaimDto> Handle(UpdateUserOperationClaimCommand request, CancellationToken cancellationToken)
    {
        var userOperationClaim = _mapper.Map<UserOperationClaim>(request);
        await _userOperationClaimBusinessRule.UserOperationClaimShouldExistWhenRequested(userOperationClaim);
        UserOperationClaim updatedClaim = await _userOperationClaimRepository.UpdateAsync(userOperationClaim);
        var mappedClaim = _mapper.Map<UpdatedUserOperationClaimDto>(updatedClaim);
        return mappedClaim;
    }
}
