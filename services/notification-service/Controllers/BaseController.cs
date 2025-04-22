using Microsoft.AspNetCore.Mvc;

namespace notification_service.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected int GetUserId()
        {
            int? userId = (int?)HttpContext.Items["UserId"];
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User not found in request headers.");
            }
            return (int)userId!;
        }

    }
}
