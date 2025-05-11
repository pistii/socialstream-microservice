using shared_libraries.Models;

namespace socialstream.IntegrationTests.Setup.TestData
{
    public static class NotificationData
    {
        public static List<Notification> GetNotificationData()
        {
            return new List<Notification>()
            {
                new Notification
                {
                    Id = 1,
                    publicId = "notif-1",
                    AuthorId = 4,
                    AuthorPublicId = "user-4",
                    AuthorAvatar = "walter.png",
                    Message = "Walter White sent you a friend request.",
                    CreatedAt = DateTime.Now.AddMinutes(-120),
                    ExpirationDate = DateTime.Now.AddDays(7),
                    NotificationType = NotificationType.FriendRequest
                },
                new Notification
                {
                    Id = 2,
                    publicId = "notif-2",
                    AuthorId = 5,
                    AuthorPublicId = "user-5",
                    AuthorAvatar = "jesse.png",
                    Message = "Jesse Pinkman accepted your friend request.",
                    CreatedAt = DateTime.Now.AddMinutes(-60),
                    ExpirationDate = DateTime.Now.AddDays(7),
                    NotificationType = NotificationType.FriendRequestAccepted
                },
                new Notification
                {
                    Id = 3,
                    publicId = "notif-3",
                    AuthorId = 6,
                    AuthorPublicId = "user-6",
                    AuthorAvatar = "tuco.png",
                    Message = "Tuco Salamanca rejected your friend request.",
                    CreatedAt = DateTime.Now.AddHours(-1),
                    ExpirationDate = DateTime.Now.AddDays(7),
                    NotificationType = NotificationType.FriendRequestRejected
                },
                new Notification
                {
                    Id = 4,
                    publicId = "notif-4",
                    AuthorId = 7,
                    AuthorPublicId = "user-7",
                    AuthorAvatar = "saul.png",
                    Message = "Happy birthday! Saul Goodman sent you a gift.",
                    CreatedAt = DateTime.Now.AddDays(-1),
                    ExpirationDate = DateTime.Now.AddDays(6),
                    NotificationType = NotificationType.Birthday
                },
                new Notification
                {
                    Id = 5,
                    publicId = "notif-5",
                    AuthorId = 4,
                    AuthorPublicId = "user-4",
                    AuthorAvatar = "walter.png",
                    Message = "Walter White posted a new article.",
                    CreatedAt = DateTime.Now,
                    ExpirationDate = DateTime.Now.AddDays(3),
                    NotificationType = NotificationType.NewPost
                },
            };
        }
    }
}
