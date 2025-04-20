using chat_service.Models;
using Microsoft.AspNetCore.Mvc;

namespace chat_service.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected int? GetUserId()
        {
            var user = HttpContext.Items["UserId"] as int?;
            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found in request headers.");
            }
            return user;
        }

    }
}
