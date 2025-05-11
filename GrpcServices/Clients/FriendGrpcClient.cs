using Grpc.Core;
using Grpc.Net.Client;
using GrpcServices.Interfaces;
using GrpcServices.Protos;

namespace GrpcServices.Clients
{
    public class FriendGrpcClient : IFriendGrpcClient
    {
        private readonly Friend.FriendClient _client;

        public FriendGrpcClient(Friend.FriendClient client)
        {
            _client = client;
        }

        public async Task<GetFriendIdsResponse> GetFriendsForUserAsync(GetFriendIdsRequest request)
        {
            return await _client.GetFriendsForUserAsync(request);
        }

        public async Task<FriendObj> CreateFriendshipIfNotExistsAsync(FriendObj request)
        {
            return await _client.CreateFriendshipIfNotExistsAsync(request);
        }

        public async Task<FriendObj> GetFriendship(FriendObj request)
        {
            return await _client.GetFriendshipAsync(request);
        }

        public async Task<FriendObj> RemoveFriendshipIfExists(FriendObj request)
        {
            return await _client.RemoveFriendshipIfExistsAsync(request);
        }

    }
}
