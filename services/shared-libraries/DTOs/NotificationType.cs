using System.ComponentModel;
namespace shared_libraries.DTOs
{
    public enum NotificationType
    {
        [Description("Friend Request Sent")]
        FriendRequest = 0,

        [Description("Friend Request Accepted")]
        FriendRequestAccepted = 1,

        [Description("Friend Request Accepted")]
        FriendRequestRejected = 2,

        [Description("Happy Birthday!")]
        Birthday = 10,

        [Description("New Post Available")]
        NewPost = 20
    }
}
