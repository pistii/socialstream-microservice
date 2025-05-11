
using shared_libraries.Interfaces;
using shared_libraries.Models;

namespace shared_libraries.Interfaces
{
    public interface INotificationRepository : IGenericRepository
    {
        Task<List<GetNotification>> GetAllNotifications(int userId);
        Task<List<Notification>> GetAllNotificationTest();
        Task<List<UserNotification>> GetAllUserNotificationTest();
        Task<GetNotification> SendNotification(int receiverUserId, Personal author, CreateNotification createNotification);
    }
}
