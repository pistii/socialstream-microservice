using Microsoft.AspNetCore.Mvc;

namespace friend_service.Controllers
{
    public class FriendController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
