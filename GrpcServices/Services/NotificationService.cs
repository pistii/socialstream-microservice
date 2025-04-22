using Grpc.Core;
using GrpcServices;
using shared_libraries.Interfaces;

namespace GrpcServices.Services
{
    public class NotificationService : Notification.NotificationBase
    {
        private readonly ITestRepository notificationRepository;
        public NotificationService(ITestRepository notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }

        public override Task<NotificationResponse> SendNotification(NotificationRequest request, ServerCallContext context)
        {
            Console.WriteLine($"New Notification: {request.Message}");
            notificationRepository.SendNotification(request.Message);
            return Task.FromResult(new NotificationResponse { Success = true });
        }
    }
}
