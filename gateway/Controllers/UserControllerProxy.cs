using GrpcServices.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace gateway.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/user")]
    [ApiController]
    public class UserControllerProxy : BaseController
    {
        public UserControllerProxy()
        {
            ClientConnectionHandler.Initialize("http://user-service:8080");
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var response = await ClientConnectionHandler.UserClient.GetUserAsync(new GrpcServices.UserRequest() { PublicId = "a1" });
            return Ok(response);
        }
    }
}
