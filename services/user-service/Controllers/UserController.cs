using GrpcServices;
using Microsoft.AspNetCore.Mvc;
using shared_libraries.Controller;
using shared_libraries.Controllers;
using shared_libraries.Interfaces;

namespace user_service.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("getUser/{publicId}")]
        public async Task<shared_libraries.Models.User?> GetUser(string publicId)
        {
            return await _userRepository.GetByPublicId(publicId);
        }

        [HttpGet("getUser/{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            Console.WriteLine("Get user with id from GetUser controller: " + userId);

            var user = await _userRepository.GetUserByIdForKafka(userId);//_userRepository.GetByIdAsync<shared_libraries.Models.User>(userId);
            var testUser = await _userRepository.GetUserByIdForKafka(1);//_userRepository.GetByIdAsync<shared_libraries.Models.User>(userId);
            Console.WriteLine("Get user with hardcoded id: " + testUser);

            if (user == null)
            {
                Console.WriteLine("User not found");
                return BadRequest(null);
            }

            else {
                var userDto = new shared_libraries.DTOs.UserDto()
                {
                    email = user.email,
                    isActivated = user.isActivated,
                    userId = user.userId,
                };
                return Ok(userDto);
            }
        }

    }
}