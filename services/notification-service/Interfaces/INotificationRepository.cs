
using notification_service.Models;
using shared_libraries.Interfaces;
using shared_libraries.Models;

namespace notification_service.Interfaces
{
    public interface INotificationRepository : IGenericRepository
    {
        Task<List<GetNotification>> GetAllNotifications(int userId);
        Task<GetNotification> SendNotification(int receiverUserId, Personal author, CreateNotification createNotification);
    }
}
