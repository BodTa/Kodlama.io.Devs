

using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;

namespace Application.Features.UserOperationClaims.Rules;

public class UserOperationClaimBusinessRule
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;

    public UserOperationClaimBusinessRule(IUserOperationClaimRepository userOperationClaimRepository)
    {
        _userOperationClaimRepository = userOperationClaimRepository;
    }
    public async Task UserOperationClaimCannotBeDuplicated(UserOperationClaim userOperationClaim)
    {
        if(userOperationClaim != null) { throw new BusinessException("User Operation Claim cannot be duplicated"); };
    }
    public async Task UserOperationClaimShouldExistWhenRequested(UserOperationClaim userOperationClaim)
    {
        if(userOperationClaim == null) { throw new BusinessException("User Operation Claim does not exist"); };
    }
}
