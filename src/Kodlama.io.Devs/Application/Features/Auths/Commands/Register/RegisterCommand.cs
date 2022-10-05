
using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auths.Commands.Register;

public class RegisterCommand:IRequest<RegisteredDto>
{
    public UserForRegisterDto UserForRegisterDto { get; set; }
    public string IpAdress { get; set; }
}
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredDto>
{
    private readonly IAuthService _authService;
    private readonly AuthBusinessRules _authBusinessRules;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IAuthService authService, AuthBusinessRules authBusinessRules, IUserRepository userRepository)
    {
        _authService = authService;
        _authBusinessRules = authBusinessRules;
        _userRepository = userRepository;
    }

    public async Task<RegisteredDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await _authBusinessRules.EmailCannotBeDuplicated(request.UserForRegisterDto.Email);
        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);
        User newUser = new()
        {
            Email = request.UserForRegisterDto.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            FirstName = request.UserForRegisterDto.FirstName,
            LastName = request.UserForRegisterDto.LastName,
            Status = true,
        };
        User createdUser = await _userRepository.AddAsync(newUser);
        AccessToken accessToken = await _authService.CreateAccessToken(createdUser);
        RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdUser, request.IpAdress);
        RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);
        RegisteredDto registeredDto = new()
        {
            AccessToken = accessToken,
            RefreshToken = createdRefreshToken
        };
        return registeredDto;

    }
}