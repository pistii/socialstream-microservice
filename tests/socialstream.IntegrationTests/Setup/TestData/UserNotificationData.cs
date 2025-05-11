using shared_libraries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socialstream.IntegrationTests.Setup.TestData
{
    public static class UserNotificationData
    {
        public static List<UserNotification> GetUserNotification()
        {
            return new List<UserNotification>
            {
                new UserNotification { PK_Id = 1, UserId = 5, NotificationId = 1, IsRead = false },
                new UserNotification { PK_Id = 2, UserId = 6, NotificationId = 1, IsRead = true },
                new UserNotification { PK_Id = 3, UserId = 4, NotificationId = 2, IsRead = false },
                new UserNotification { PK_Id = 4, UserId = 7, NotificationId = 3, IsRead = false },
                new UserNotification { PK_Id = 5, UserId = 8, NotificationId = 4, IsRead = true },
                new UserNotification { PK_Id = 6, UserId = 9, NotificationId = 4, IsRead = false },
                new UserNotification { PK_Id = 7, UserId = 10, NotificationId = 5, IsRead = false },
                new UserNotification { PK_Id = 8, UserId = 5, NotificationId = 5, IsRead = true },
                new UserNotification { PK_Id = 9, UserId = 6, NotificationId = 5, IsRead = false },
            };
        }
    }
}
