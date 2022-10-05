using Application.Features.Auths.Commands.Login;
using Application.Features.Auths.Commands.Register;
using Application.Features.Auths.Dtos;
using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Queries.UserLogin;
using Core.Security.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
    {
        LoginCommand loginCommand = new() { UserForLoginDto = userForLoginDto,IpAdress=GetIpAddress() };
        LoginDto result = await Mediator.Send(loginCommand);
        SetRefreshTokenToCookie(result.RefreshToken);
        return Ok(result.AccessToken);
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
    {
        RegisterCommand registerCommand = new() { UserForRegisterDto = userForRegisterDto, IpAdress=GetIpAddress() };
        RegisteredDto result = await Mediator.Send(registerCommand);
        SetRefreshTokenToCookie(result.RefreshToken);
        return Created("", result.AccessToken);
    }
    private void SetRefreshTokenToCookie(RefreshToken refreshToken)
    {
        CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.Now.AddDays(7) };
        Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
    }
}
