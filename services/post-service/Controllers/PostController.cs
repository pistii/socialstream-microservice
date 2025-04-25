using Microsoft.AspNetCore.Mvc;

namespace post_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly PostDbContext _postDbContext;
        public PostController(PostDbContext postDbContext)
        {
            _postDbContext = postDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_postDbContext.Post.FirstOrDefault());
        }
    }
}
