using gateway.DTOs;
using shared_libraries.Interfaces;

namespace gateway.Interfaces
{
    public interface ITestRepository
    {
        Task<List<GetNotification>> GetAllNotifications(int userId);
        Task SendNotification(string message);
    }
}
