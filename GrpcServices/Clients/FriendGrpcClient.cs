using Grpc.Net.Client;
using GrpcServices.Interfaces;
using GrpcServices.Protos;

namespace GrpcServices.Clients
{
    public class FriendGrpcClient : IFriendGrpcClient
    {
        private readonly Friend.FriendClient _client;

        public FriendGrpcClient(GrpcChannel channel)
        {
            _client = new Friend.FriendClient(channel);
        }

        public async Task<GetFriendIdsResponse> GetFriendsForUserAsync(GetFriendIdsRequest request)
        {
            return await _client.GetFriendsForUserAsync(request);
        }

        public async Task<FriendObj> CreateFriendshipIfNotExistsAsync(FriendObj request)
        {
            return await _client.CreateFriendshipIfNotExistsAsync(request);
        }
    }

}
