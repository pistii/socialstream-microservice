using GrpcServices.Protos;

namespace GrpcServices.Interfaces
{
    public interface IFriendGrpcClient
    {
        Task<GetFriendIdsResponse> GetFriendsForUserAsync(GetFriendIdsRequest request);
        Task<FriendObj> CreateFriendshipIfNotExistsAsync(FriendObj request);
    }
}
