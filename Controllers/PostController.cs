using FirstDotNet.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstDotNet.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("getPosts")]
        public async Task<IActionResult> GetPosts()
        {
            return Ok(await _postService.GetPosts());
            
        }

    }
}
