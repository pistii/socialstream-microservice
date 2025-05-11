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
            _friendDbContext = friendDbContext;
        }

        public async Task<List<Friend>> GetAllTest()
        {
            return await  _friendDbContext.Friendship.ToListAsync();
        }


        public async Task<Personal> GetUserWithNotification(int userId)
        {
            return null;
            //var user = await _friendDbContext.Personal
            //    .Include(u => u.users)
            //    .Include(p => p.UserNotification)
            //    .ThenInclude(n => n.notification)
            //    .FirstOrDefaultAsync(p => p.id == userId);
            //return user;
        }


        public async Task<IEnumerable<int>> GetAllFriendIds(int id)
        {
            if (id == 0 || id == null) throw new Exception("DEBUG: SÁLÁLÁLÁLÁLÁ AZ ID NULL!!!!!!!");


            Console.WriteLine("DEBUG: Most kiderül hogy a logika a rossz, vagy üres a friend adatbázis: ");
            var friends = await _friendDbContext.Friendship.ToListAsync();
            foreach (var friend in friends) {
                Console.WriteLine($"FriendId:{friend.FriendId}|UserId:{friend.UserId}|statusId:{friend.StatusId}");
            }


            var listFriendIds = await _friendDbContext.Friendship
                .Where(f => (f.FriendId == id || f.UserId == id) && f.StatusId == 1)
                .Select(f => f.UserId == id ? f.FriendId : f.UserId)
                .ToListAsync();

            Console.WriteLine($"DEBUG: ennyi barátot találtam: ({listFriendIds.Count})");
            foreach (var item in listFriendIds)
            {
                Console.WriteLine(item);
            }
            return listFriendIds;
        }

        public async Task<IEnumerable<Personal>> GetAllFriendAsync(int userId)
        {
            return null;
            //var friends = await _context.Personal.Include(user => user.users)
            //.Where(p => _context.Friendship
            //    .Where(f => (f.FriendId == userId || f.UserId == userId) && f.StatusId == 1)
            //    .Select(f => f.UserId == userId ? f.FriendId : f.UserId)
            //    .Contains(p.id)
            //)
            //.ToListAsync();
            //return friends;
        }

        public async Task<IEnumerable<Personal>> GetFriendsForInitialUserData(int userId)
        {
            //var friends = await _context.Personal.Include(user => user.users)
            //.Where(p => _context.Friendship
            //    .Where(f => (f.FriendId == userId || f.UserId == userId) && f.StatusId == 1)
            //    .Select(f => f.UserId == userId ? f.FriendId : f.UserId)
            //    .Contains(p.id)
            //)
            //.Take(10)
            //.ToListAsync();
            //return friends;
            return null;

        }

        public async Task<UserRelationshipStatus> GetRelationStatusAsync(int userA, int userB)
        {
            if (userA == userB) return UserRelationshipStatus.Self;

            var relation = await _friendDbContext.Friendship
                .FirstOrDefaultAsync(f =>
                    (f.UserId == userA && f.FriendId == userB) ||
                    (f.UserId == userB && f.FriendId == userA));

            if (relation == null)
                return UserRelationshipStatus.Stranger;

            if (relation.StatusId == 1) return UserRelationshipStatus.Friend;
            if (relation.StatusId == 4 && relation.UserId == userA)
                return UserRelationshipStatus.FriendRequestRejected;
            if (relation.StatusId == 3 && relation.UserId == userB) return UserRelationshipStatus.FriendRequestSent;
            if (relation.StatusId == 3 && relation.FriendId == userB) return UserRelationshipStatus.FriendRequestReceived;

            if (relation.StatusId == 6)
                return UserRelationshipStatus.Blocked;

            return UserRelationshipStatus.Stranger;
        }

        public void Delete(Friend request)
        {
            _friendDbContext.Friendship.Remove(request);
        }

        public async Task<Friend?> FriendshipExists(Friend friendship)
        {
            return await _friendDbContext.Friendship
                .FirstOrDefaultAsync(friend =>
                friend.FriendId == friendship.FriendId && friend.UserId == friendship.UserId ||
                friend.FriendId == friendship.UserId && friend.UserId == friendship.FriendId);
        }

        public async Task<List<int>> GetFriendIds(int userId)
        {
            return await _friendDbContext.Friendship.Where(friend =>
                friend.FriendId == userId && friend.StatusId == 1 ||
                friend.UserId == userId && friend.StatusId == 1)
                .Select(u => u.FriendId == userId ? u.UserId : u.FriendId)
                .ToListAsync();
        }

        public Task<IEnumerable<int>> GetAll(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFriendshipIfExists(Friend friend)
        {
            var friendship = await FriendshipExists(friend);
            if (friendship == null) return false;
            Delete(friendship);
            await SaveAsync(); 
            return true;
        }
    }
}
