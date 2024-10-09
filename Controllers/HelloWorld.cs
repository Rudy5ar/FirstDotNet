using FirstDotNet.Context;
using FirstDotNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloWorld : ControllerBase
    {

        public readonly SocialMediaContext _context;

        public HelloWorld(SocialMediaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _context.Add(new User
            {
                Username = "opop",
                Email = "aad@asdasd.com",
                Password = "123",
            });
            _context.SaveChanges();
            return Ok("Hello World");
        }

    }
}
