using Grpc.Core;
using GrpcServices.Protos;

namespace GrpcServices.Interfaces
{
    public interface IFriendGrpcClient
    {
        Task<GetFriendIdsResponse> GetFriendsForUserAsync(GetFriendIdsRequest request);
        Task<FriendObj> CreateFriendshipIfNotExistsAsync(FriendObj request);
        Task<FriendObj> GetFriendship(FriendObj request);
        Task<FriendObj> RemoveFriendshipIfExists(FriendObj request);

    }
}
