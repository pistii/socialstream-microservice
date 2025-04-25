using Grpc.Core;
using GrpcServices;
using shared_libraries.Interfaces;
using shared_libraries.Models;

namespace GrpcServices.Services
{
    public class NotificationService : Notification.NotificationBase
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public override Task<NotificationResponse> SendNotification(NotificationRequest request, ServerCallContext context)
        {
            Console.WriteLine($"New Notification: {request.Message}");
            //_notificationRepository.SendNotification(request.Message);
            return Task.FromResult(new NotificationResponse { Success = true });
        }

        public async override Task<GetAllNotificationsResponse> GetAllNotification(NotificationRequest request, ServerCallContext context)
        {
            if (!int.TryParse(request.PublicId, out int userId))
            {
                throw new ArgumentException("Invalid user ID format.");
            }

            var notifications = await _notificationRepository.GetAllNotifications(userId);

            var response = new GetAllNotificationsResponse();


            response.Notification.AddRange(notifications.Select(n => new GetNotificationResponse
            {
                Message = n.Message,
                AuthorId = n.AuthorId,
                PublicId = n.NotificationId
            }));

            return response;
        }

    }
}
