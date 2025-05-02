using Google.Protobuf.Collections;
using GrpcServices;
using GrpcServices.Handlers;
using GrpcServices.Mappers;
using GrpcServices.Services;
using Microsoft.AspNetCore.Mvc;
using shared_libraries.Kafka;
using shared_libraries.Models;

namespace gateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FriendControllerProxy : BaseController
    {
        public FriendControllerProxy()
        {
            ClientConnectionHandler.Initialize("http://user-service:8080");
            ClientConnectionHandler.Initialize("http://friend-service:8080");
        }


        [HttpGet("getAll/{publicId}/{currentPage}/{qty}")]
        public async Task<IActionResult> GetAll(string publicId, int currentPage = 1, int qty = 9)
        {
            var userWithPublicId = await ClientConnectionHandler.UserClient.GetUserAsync(
                new GrpcServices.UserRequest()
                { PublicId = publicId });

            if (userWithPublicId == null) return NotFound("User not found");

            var friendIds = await ClientConnectionHandler.FriendClient.GetFriendsForUserAsync(
                new GrpcServices.Protos.GetFriendIdsRequest()
            {
                UserId = userWithPublicId.UserId
            });

            if (friendIds == null) return BadRequest("Request failed.");
            if (!friendIds.Success) return NotFound();

            //var users = await ClientConnectionHandler.UserClient.GetUsers
            var getAllUserRequestParam = new GetAllUserRequest();
            getAllUserRequestParam.UserIds.AddRange(friendIds.Friends);

            var friends = await ClientConnectionHandler.UserClient.GetAllUserByIdAsync(getAllUserRequestParam);
            if (friends == null) return NotFound("No users found");
            if (!friends.Success) return BadRequest("Failed to request friends");


            var mappedData = ObjectMapper.Map<RepeatedField<UserResponse>, List<Personal>>(friends.Users);

            var sorted = mappedData?.Skip((currentPage - 1) * qty).Take(qty);

            return Ok(sorted);
        }
    }
}
