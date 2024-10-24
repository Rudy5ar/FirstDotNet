using FirstDotNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FirstDotNet.Services {
    public class LoginRequest {
        public required string username { get; set; }
        public required string password { get; set; }
    }

    public class RegisterRequest {
        public required string email { get; set; }
        public required string username { get; set; }
        public required string password { get; set; }
    }

    [Route("api/auth")]
    [ApiController]
    public class Authentication : ControllerBase {

        private readonly IUserService _userService;
        public Authentication(IUserService userService){
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginRequest loginRequest) {
            if (await _userService.loginUser(loginRequest) == null) {
                return BadRequest();
            }

            return Ok(GenerateJwtToken(loginRequest.username, new List<Claim>()));
        }

        [HttpPost("register")]
        public async Task<IActionResult> register([FromBody] RegisterRequest request) {
            var registeredUser = await _userService.registerUser(request);
            if (registeredUser == null) {
                return BadRequest();
            }
            return Ok(registeredUser);
        }

        [HttpGet]
        public string GenerateJwtToken(string username, IEnumerable<Claim> claims) {
            var tokenClaims = new List<Claim>(claims) {
                new Claim(ClaimTypes.Name, username)
            };

            var token = new JwtSecurityToken(
                issuer: "https://localhost:8080",
                audience: "FirstDotNetAudience",
                expires: DateTime.Now.AddMinutes(30),
                claims: tokenClaims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
