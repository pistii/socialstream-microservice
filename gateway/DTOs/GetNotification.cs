using shared_libraries.DTOs;
using System.ComponentModel.DataAnnotations;

namespace gateway.DTOs
{
    public class GetNotification
    {
        public GetNotification(string notificationId, string authorId, string receiverId, DateTime createdAt, string message, bool isRead, NotificationType notificationType, string notificationDescription, string avatar)
        {
            this.NotificationId = notificationId;
            this.AuthorId = authorId;
            this.ReceiverUserId = receiverId;
            this.CreatedAt = createdAt;
            this.Message = message;
            this.IsRead = isRead;
            this.NotificationType = notificationType;
            this.notificationDescription = notificationDescription;
            this.Avatar = avatar;
        }
        public string NotificationId { get; set; }
        public string AuthorId { get; set; }
        public string ReceiverUserId { get; set; }
        [StringLength(300)]
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public NotificationType NotificationType { get; set; }
        public string notificationDescription { get; set; }
        public string? Avatar { get; set; }
    }
}
