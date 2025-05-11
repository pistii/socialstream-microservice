using Grpc.Core;
using GrpcServices;
using GrpcServices.Interfaces;
using GrpcServices.Mappers;
using shared_libraries.Interfaces;
using shared_libraries.Models;
using dbModel = shared_libraries.Models;


namespace GrpcServices.Services
{
    public class NotificationService : GrpcNotification.GrpcNotificationBase
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(
            INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public Task<NotificationResponse> SendNotification(NotificationRequest request, ServerCallContext context)
        {
            Console.WriteLine($"New Notification: {request.Message}");
            //_notificationRepository.SendNotification(request.Message);
            return Task.FromResult(new NotificationResponse { Success = true });
        }

        public async override Task<GetAllNotificationsResponse> GetAllNotification(GetNotificationByUserPrivateId request, ServerCallContext context)
        {
            var notifications = await _notificationRepository.GetAllNotifications(request.UserId);
            
            var response = new GetAllNotificationsResponse();


            response.Notification.AddRange(notifications.Select(n => new GetNotificationResponse
            {
                Message = n.Message,
                AuthorPublicId = n.AuthorId,
                PublicId = n.NotificationId
            }));

            return response;
            
        }

        public async override Task<NotificationResponse> RemoveFriendRequestNotification(RemoveFriendRequestNotificationRequest request, ServerCallContext context)
        {
            try
            {
                var notification = new dbModel.Notification()
                {
                    NotificationType = NotificationType.FriendRequest,
                    AuthorId = request.UserId
                };
                await _notificationRepository.RemoveThenSaveAsync(notification);
                return new NotificationResponse() { Success = true };
            }
            catch (Exception) 
            {
                return new NotificationResponse() { Success = false };
            }
        }

        public async override Task<NotificationResponse> GetNotificationByPublicId(NotificationRequest request, ServerCallContext context)
        {
            var notification = await _notificationRepository.GetByPublicIdAsync<shared_libraries.Models.Notification>(request.PublicId);

            if (notification == null)
            {
                return new NotificationResponse() { Success = false };
            }

            var mappedData = ObjectMapper.Map<dbModel.Notification, NotificationResponse>(notification);
            mappedData.Success = true;
            if (mappedData is not NotificationResponse) 
                throw new Exception("Nem sikerült az értesítést átalakítani a GetNotificationByPublicId metódusban.");
                
            return mappedData;
        }


        public async override Task<Status> RemoveNotificationById(NotificationRequest request, ServerCallContext context)
        {
            var notification = await _notificationRepository.GetByPublicIdAsync<dbModel.Notification>(request.PublicId);
            if (notification == null) return new Status() { Success = false };
            await _notificationRepository.RemoveThenSaveAsync(notification);
            return new Status() { Success = true };
        }

        public async override Task<NotificationResponse> GetNotificationByTypeAndUserId(RemoveFriendRequestNotificationRequest request, ServerCallContext context)
        {
            var notification = await _notificationRepository.GetEntityByPredicateFirstOrDefaultAsync<dbModel.Notification>(p => p.NotificationType == NotificationType.FriendRequest && p.AuthorId == request.UserId);
            if (notification == null) return new NotificationResponse() { Success = false };
            var mappedData = ObjectMapper.Map<dbModel.Notification, NotificationResponse>(notification);
            mappedData.Success = true;
            return mappedData;
        }

        public async override Task<NotificationResponse> CreateNotification(CreateNotificationRequest request, ServerCallContext context)
        {
            try
            {
                var notification = new dbModel.Notification(
                    request.AuthorId,
                    request.Message,
                    Enum.Parse<NotificationType>(request.NotificationType.ToString()),
                    request.AuthorPublicId,
                    request.AuthorAvatar,
                    request.ReceiverId);

                await _notificationRepository.InsertSaveAsync(notification);
                return new NotificationResponse() { Success = true };
            }
            catch (Exception ex) {
                return new NotificationResponse() { Success = false };
            }
        }
    }
}
