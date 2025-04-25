using GrpcServices;
using Microsoft.AspNetCore.Mvc;
using shared_libraries.Controller;

namespace user_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        public async Task GetUserAsync(UserRequest userRequest)
        {
            Console.WriteLine("Received message from gateway");
            Console.WriteLine(userRequest.PublicId);
        }
    }
}