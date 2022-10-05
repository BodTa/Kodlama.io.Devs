

using Application.Services.Repositories;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.JWT;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.AuthService;

public class AuthManager : IAuthService
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenHelper _tokenHelper;

    public AuthManager(IUserOperationClaimRepository userOperationClaimRepository, IRefreshTokenRepository refreshTokenRepository, ITokenHelper tokenHelper)
    {
        _userOperationClaimRepository = userOperationClaimRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _tokenHelper = tokenHelper;
    }

    public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
    {
        RefreshToken addedRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
        return addedRefreshToken;
    }

    public async Task<AccessToken> CreateAccessToken(User user)
    {

        //Getting User Operation Claims.
        IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(
            u => u.UserId == user.Id,
            include: u => u.Include(u => u.OperationClaim));
        // Getting  Operation Claims from User Operation Claims.
        IList<OperationClaim> operationClaims = userOperationClaims.Items.Select(u=> new OperationClaim
        {
            Id = u.OperationClaimId,
            Name= u.OperationClaim.Name
        }).ToList();
        // Getting Token from TokenManager and returning.
        AccessToken accessToken = _tokenHelper.CreateToken(user, operationClaims);
        return accessToken;
    }

    public Task<RefreshToken> CreateRefreshToken(User user, string ipAdress)
    {
        RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(user, ipAdress);
        return Task.FromResult(refreshToken);
    }
}
