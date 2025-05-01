using shared_libraries.Interfaces;
using shared_libraries.Models;
using shared_libraries.Services;

namespace friend_service.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        public void Delete(Friend request)
        {
            throw new NotImplementedException();
        }

        public Task<Friend?> FriendshipExists(Friend friendship)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PersonalIsOnlineDto>> GetAll(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Personal>> GetAllFriendAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetByIdAsync<T>(int id) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Personal>> GetFriendsForInitialUserData(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserRelationshipStatus> GetRelationStatusAsync(int userA, int userB)
        {
            throw new NotImplementedException();
        }

        public Task<Personal?> GetUserWithNotification(int userId)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync<T1>(T1 entity) where T1 : class
        {
            throw new NotImplementedException();
        }

        public Task InsertSaveAsync<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public List<T> Paginator<T>(List<T> sortable, int currentPage = 1, int messagePerPage = 20) where T : class
        {
            throw new NotImplementedException();
        }

        public void Remove<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task RemoveThenSaveAsync<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateThenSaveAsync<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        Task<T?> IGenericRepository.GetByPublicIdAsync<T>(string publicId) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
