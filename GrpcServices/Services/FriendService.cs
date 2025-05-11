using Grpc.Core;
using GrpcServices.Protos;
using shared_libraries.Interfaces;
using dbModel = shared_libraries.Models;

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

        public async override Task<FriendObj> CreateFriendshipIfNotExists(FriendObj request, ServerCallContext context)
        {
            var friend = new dbModel.Friend(request.ReceiverId, request.AuthorId, request.FriendshipStatusId);
            var existing = await _friendRepository.FriendshipExists(friend);

            //The other user requested before, mark them as friends.
            if (existing != null && friend.UserId == request.ReceiverId)
            {
                existing.StatusId = 1;
                await _friendRepository.UpdateThenSaveAsync<dbModel.Friend>(existing);
            }
            else
            {
                friend.FriendshipSince = DateTime.Now;
                await _friendRepository.InsertSaveAsync(friend);
            }

            return new FriendObj()
            {
                FriendshipStatusId = existing.StatusId ?? 0,
                AuthorId = existing.UserId,
                ReceiverId = request.ReceiverId,
            };
        }

        public async override Task<FriendObj> GetFriendship(FriendObj request, ServerCallContext context)
        {
            var friend = new dbModel.Friend(request.ReceiverId, request.AuthorId, request.FriendshipStatusId);

            var friendship = await _friendRepository.FriendshipExists(friend);
            if (friendship != null)
            {
                return new FriendObj()
                {
                    AuthorId = friendship.UserId,
                    ReceiverId = friendship.FriendId,
                    FriendshipStatusId = friendship.StatusId ?? 2,
                    Success = true
                };
            }

            return new FriendObj()
            {
                Success = false,
            };
        }

        public async override Task<FriendObj> RemoveFriendshipIfExists(FriendObj request, ServerCallContext context)
        {
            var friend = new dbModel.Friend(request.ReceiverId, request.AuthorId, request.FriendshipStatusId);
            var removed = await _friendRepository.RemoveFriendshipIfExists(friend);
            if (removed) return new FriendObj() { Success = true };
            return new FriendObj() { Success = false };
        }

    }
}
