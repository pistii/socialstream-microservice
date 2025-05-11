using shared_libraries.Models;
using shared_libraries.Services;

namespace shared_libraries.Interfaces
{
    public interface IFriendRepository : IGenericRepository
    {
        Task<IEnumerable<Personal>> GetAllFriendAsync(int id);
        Task<IEnumerable<Personal>> GetFriendsForInitialUserData(int userId);
        public Task<Friend?> FriendshipExists(Friend friendship);
        Task<Personal?> GetUserWithNotification(int userId);
        Task<UserRelationshipStatus> GetRelationStatusAsync(int userA, int userB);
        void Delete(Friend request);
        Task<IEnumerable<int>> GetAll(int id);
        Task<IEnumerable<int>> GetAllFriendIds(int id);
        Task<bool> RemoveFriendshipIfExists(Friend friend);
        Task<List<Friend>> GetAllTest();
    }
}
