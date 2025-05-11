using Grpc.Core;

namespace GrpcServices.Interfaces
{
    public interface INotificationGrpcClient
    {
        Task<GetAllNotificationsResponse> GetAllNotification(GetNotificationByUserPrivateId request);
        Task<NotificationResponse> RemoveFriendRequestNotificationRequest(RemoveFriendRequestNotificationRequest request);
        Task<NotificationResponse> SendNotification(NotificationRequest request, ServerCallContext context);
        //Task<NotificationResponse> UpdateOrCreateFriendRequestNotification(GetExistingNotificationRequest request);
        Task<NotificationResponse> GetNotificationByPublicId(NotificationRequest request);
        Task<Status> RemoveNotificationById(NotificationRequest request);

        Task<NotificationResponse> CreateNotification(CreateNotificationRequest request);
    }
}
