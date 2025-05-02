using Microsoft.EntityFrameworkCore;
using shared_libraries.Interfaces;
using shared_libraries.Models;
using shared_libraries.Repositories;
using shared_libraries.Services;

namespace friend_service.Repositories
{
    public class FriendRepository : GenericRepository<FriendDbContext>, IFriendRepository
    {
        private readonly FriendDbContext _friendDbContext;

        public FriendRepository(FriendDbContext friendDbContext) : base(friendDbContext)
        {

        }

        public Task<IEnumerable<PersonalIsOnlineDto>> GetAll(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<int>> GetAllFriendIds(int id)
        {
            var listFriendIds = await _friendDbContext.Friendship
                .Where(f => (f.FriendId == id || f.UserId == id) && f.StatusId == 1)
                .Select(f => f.UserId == id ? f.FriendId : f.UserId)
                .ToListAsync();

            return listFriendIds;
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
