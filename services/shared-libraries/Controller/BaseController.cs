using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared_libraries.Controller
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
