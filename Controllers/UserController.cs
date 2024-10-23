using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FirstDotNet.Services;

namespace FirstDotNet.Controllers;

public interface ILoginRequest
{
    string username { get; set; }
    string password { get; set; }
}

public class LoginRequest : ILoginRequest
{
    public required string username { get; set; }
    public required string password { get; set; }
}

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{

    private readonly Authentication _authentication;
    private readonly IUserService _userService;
    public UserController(Authentication authentication, IUserService userService)
    {
        _authentication = authentication;
        _userService = userService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        if (_userService.IsValidUser(loginRequest.username, loginRequest.password))
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginRequest.username),
            };

            var token = _authentication.GenerateJwtToken(loginRequest.username, claims);
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }
}
