
using shared_libraries.Interfaces;
using shared_libraries.Models;

namespace shared_libraries.Interfaces
{
    public interface INotificationRepository : IGenericRepository
    {
        Task<List<GetNotification>> GetAllNotifications(int userId);
        Task<GetNotification> SendNotification(int receiverUserId, Personal author, CreateNotification createNotification);
    }
}
