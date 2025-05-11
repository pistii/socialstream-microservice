using shared_libraries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socialstream.IntegrationTests.Setup.TestData
{
    public static class FriendData
    {
        public static List<Friend> GetFriends()
        {
            return new List<Friend>()
            {
                new Friend()
                {
                    FriendshipID = 1,
                    UserId = 4, // Walter
                    FriendId = 5, // Jesse
                    StatusId = 1, // friends
                    FriendshipSince = DateTime.Now.AddMonths(-11)
                },
                new Friend()
                {
                    FriendshipID = 2,
                    UserId = 6, // Tuco
                    FriendId = 5, // Jesse
                    StatusId = 4, // rejected
                    FriendshipSince = DateTime.Now.AddMonths(-6)
                },
                new Friend()
                {
                    FriendshipID = 3,
                    UserId = 7, // Saul
                    FriendId = 4, // Walter
                    StatusId = 3, // sent
                    FriendshipSince = DateTime.Now.AddDays(-2)
                },
                new Friend()
                {
                    FriendshipID = 4,
                    UserId = 9, // Lydia
                    FriendId = 6, // Tuco
                    StatusId = 1, // friends
                    FriendshipSince = DateTime.Now.AddMonths(-3)
                },
                new Friend()
                {
                    FriendshipID = 5,
                    UserId = 10, // Mike
                    FriendId = 4, // Walter
                    StatusId = 1, // friends
                    FriendshipSince = DateTime.Now.AddMonths(-5)
                },
                new Friend()
                {
                    FriendshipID = 6,
                    UserId = 8, // Skyler
                    FriendId = 5, // Jesse
                    StatusId = 2, // nonFriend
                    FriendshipSince = null
                }
            };
        }
    }
}
