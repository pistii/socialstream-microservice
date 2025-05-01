using Microsoft.AspNetCore.Mvc;
using shared_libraries.Controllers;
using shared_libraries.Interfaces;
using shared_libraries.Kafka;
using shared_libraries.Models;

namespace friend_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : BaseController
    {
        private readonly IFriendRepository _friendRepository;
        public FriendController(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        //[HttpGet("getAll/{publicId}/{currentPage}/{qty}")]
        [HttpGet("kafka-test")]
        public async Task<IActionResult> GetAll()//string publicId, int currentPage = 1, int qty = 9)
        {
            var producer = new KafkaProducerService<Friend>("kafka:9092", "getall-friend-topic");
            await producer.ProduceAsync(new Friend { UserId = 1 });

            return Ok();
            //var user = await _friendRepository.GetByPublicIdAsync<Friend>(publicId);
            //if (user == null) return BadRequest("Invalid user Id");

            //var friends = await _friendRepository.GetAllFriendAsync(user.userID);
            //var sorted = friends?.Skip((currentPage - 1) * qty).Take(qty);

            //return Ok(sorted);
        }
        {
            return Ok(_friendDbContext.Friendship.FirstOrDefault());
        }
    }
}
