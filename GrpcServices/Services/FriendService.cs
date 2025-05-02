using Grpc.Core;
using GrpcServices.Protos;
using shared_libraries.Interfaces;

namespace GrpcServices.Services
{
    public class FriendService : Friend.FriendBase
    {
        private readonly IFriendRepository _friendRepository;
        public FriendService(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }


        public async override Task<GetFriendIdsResponse> GetFriendsForUser(GetFriendIdsRequest request, ServerCallContext context)
        {
            var userIds = await _friendRepository.GetAllFriendIds(request.UserId);

            if (userIds.Any())
            {
                var response = new GetFriendIdsResponse()
                {
                    Success = true
                };
                response.Friends.AddRange(userIds);

                return response;
            }
            else
            {
                return new GetFriendIdsResponse()
                {
                    Success = false
                };
            }
        }
    }
}
