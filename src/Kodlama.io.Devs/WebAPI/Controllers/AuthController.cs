using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Queries.UserLogin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginQuery userLoginQuery)
    {
        var response = await Mediator.Send(userLoginQuery);
        return Ok(response);
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserCreateCommand userCreateCommand)
    {
        var response = await Mediator.Send(userCreateCommand);
        return Ok(response);
    }
}
