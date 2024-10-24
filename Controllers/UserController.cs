using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FirstDotNet.Services;

namespace FirstDotNet.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{

    private readonly Authentication _authentication;
    
}
