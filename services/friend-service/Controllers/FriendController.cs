using Microsoft.AspNetCore.Mvc;

namespace friend_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : Controller
    {
        private readonly FriendDbContext _friendDbContext;
        public FriendController(FriendDbContext friendDbContext)
        {
            _friendDbContext = friendDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_friendDbContext.Friendship.FirstOrDefault());
        }
    }
}
