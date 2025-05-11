using GrpcServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace gateway.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/user")]
    [ApiController]
    public class UserControllerProxy : BaseController
    {
        private readonly IUserGrpcClient _userGrpcClient;
        public UserControllerProxy(IUserGrpcClient userGrpcClient)
        {
            _userGrpcClient = userGrpcClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var response = await _userGrpcClient.GetUserAsync(new GrpcServices.UserRequest() { PublicId = "a1" });
            return Ok(response);
        }
    }
}
