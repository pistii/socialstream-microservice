using Grpc.Core;
using GrpcServices.Interfaces;

namespace GrpcServices.Clients
{
    public class NotificationGrpcClient : INotificationGrpcClient
    {
        private readonly GrpcNotification.GrpcNotificationClient _notificationClient;

        public NotificationGrpcClient(GrpcNotification.GrpcNotificationClient notificationClient)
        {
            _notificationClient = notificationClient;
        }

        public async Task<GetAllNotificationsResponse> GetAllNotification(GetNotificationByUserPrivateId request)
        {
            return await _notificationClient.GetAllNotificationAsync(request);
        }

        public async Task<NotificationResponse> RemoveFriendRequestNotificationRequest(RemoveFriendRequestNotificationRequest request)
        {
            return await _notificationClient.RemoveFriendRequestNotificationAsync(request);
        }

        public async Task<NotificationResponse> SendNotification(NotificationRequest request, ServerCallContext context)
        {
            return await _notificationClient.SendNotificationAsync(request);
        }

        //public async Task<NotificationResponse> UpdateOrCreateFriendRequestNotification(GetExistingNotificationRequest request)
        //{
        //    //return await _notificationClient.UpdateOrCreateFriendRequestNotificationAsync(request);
        //}

        public async Task<NotificationResponse> GetNotificationByPublicId(NotificationRequest request)
        {
            return await _notificationClient.GetNotificationByPublicIdAsync(request);
        }

        public async Task<Status> RemoveNotificationById(NotificationRequest request)
        {
            return await _notificationClient.RemoveNotificationByIdAsync(request);
        }

        public async Task<NotificationResponse> GetNotificationByTypeAndUserId(RemoveFriendRequestNotificationRequest request)
        {
            return await _notificationClient.GetNotificationByTypeAndUserIdAsync(request);
        }

        public async Task<NotificationResponse> CreateNotification(CreateNotificationRequest request)
        {
            return await _notificationClient.CreateNotificationAsync(request);
        }
    }
}
