using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FirstDotNet.Services
{
    [Route("api/auth")]
    [ApiController]
    public class Authentication : ControllerBase
    {
        [HttpGet]
        public string GenerateJwtToken(string username, IEnumerable<Claim> claims)
        {
            var tokenClaims = new List<Claim>(claims)
            {
                new Claim(ClaimTypes.Name, username)
            };

            var token = new JwtSecurityToken(
                issuer: "https://localhost:7179",
                audience: "FirstDotNetAudience",
                expires: DateTime.Now.AddMinutes(30),
                claims: tokenClaims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
