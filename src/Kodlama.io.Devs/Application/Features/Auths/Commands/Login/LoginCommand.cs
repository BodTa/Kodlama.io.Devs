using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auths.Commands.Login;

public class LoginCommand:IRequest<LoginDto>
{
    public UserForLoginDto UserForLoginDto { get; set; }
    public string IpAdress { get; set; }
}
public class LoginCommandHandler:IRequestHandler<LoginCommand,LoginDto>
{
    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;

    public LoginCommandHandler(IAuthService authService, AuthBusinessRules authBusinessRules, IUserRepository userRepository)
    {
        _authService = authService;
        _userRepository = userRepository;
    }

    public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetAsync(u => u.Email.ToLower() == request.UserForLoginDto.Email.ToLower());
        if (!HashingHelper.VerifyPasswordHash(request.UserForLoginDto.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new BusinessException("Given Email or Password wrong.");
        }
        AccessToken accessToken = await _authService.CreateAccessToken(user);
        RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IpAdress);
        RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);
        LoginDto loginDto = new() { RefreshToken = createdRefreshToken,AccessToken = accessToken };
        return loginDto;
    }
}
