using shared_libraries.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shared_libraries.Models
{
    public class UserNotification
    {
        public UserNotification()
        {

        }

        public UserNotification(int UserId, int NotificationId, bool isRead)
        {
            this.UserId = UserId;
            this.NotificationId = NotificationId;
            this.IsRead = isRead;
        }
        [Key]
        public int PK_Id { get; set; }
        public int UserId { get; set; }
        public int NotificationId { get; set; }
        public bool IsRead { get; set; }

        [ForeignKey("NotificationId")]
        public Notification? notification { get; set; }
    }
}
