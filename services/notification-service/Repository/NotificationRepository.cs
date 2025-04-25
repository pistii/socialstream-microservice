using notification_service.Repository;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using shared_libraries.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using shared_libraries.Services;
using shared_libraries.Interfaces;

namespace notification_service.Repository
{
    public class NotificationRepository : GenericRepository, INotificationRepository
    {
        private readonly NotificationDBContext _context;

        public NotificationRepository(
            NotificationDBContext context) : base(context)
        {
            _context = context;
        }


        public async Task<GetNotification> SendNotification(int receiverUserId, Personal author, CreateNotification createNotification)
        {
            var notification = new Notification(createNotification.AuthorId, "", createNotification.NotificationType,
                author.User!.PublicId,
                author.avatar ?? "",
                createNotification.UserId);
            await InsertSaveAsync(notification);

            GetNotification getNotification = new(notification);
            return getNotification;
        }
        public static Task SendNotification(string message)
        {
            Console.WriteLine("message received in notification repository" + message);
            return Task.CompletedTask;
        }

        /// <summary>
        //  Search the Person's notifications, and create a new Dto from the inherited notification class
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetNotification>> GetAllNotifications(int userId)
        {
            return await _context.UserNotification
                .Include(n => n.notification)
                .Where(n => n.UserId == userId)
                .Select(n => new GetNotification(
                        n.notification!.PublicId,
                        n.notification.AuthorPublicId,
                        string.Empty,
                        n.notification.CreatedAt,
                        n.notification.Message,
                        n.IsRead,
                        n.notification.NotificationType,
                        HelperService.GetEnumDescription(n.notification.NotificationType),
                        string.Empty))
                .ToListAsync();

            //return await _context.UserNotification
            //    .Include(n => n.notification)
            //        .ThenInclude(n => n.personal) //Ez a szerző 
            //            .ThenInclude(p => p.users)
            //    .Include(n => n.personal) //Ez a receiver
            //    .Where(n => n.UserId == userId).Select(n => new GetNotification(
            //            n.notification.PublicId,
            //            n.notification.personal.users.PublicId,
            //            n.personal.users.PublicId,
            //            n.notification.CreatedAt,
            //            n.notification.Message,
            //            n.IsRead,
            //            n.notification.NotificationType,
            //            HelperService.GetEnumDescription(n.notification.NotificationType),
            //            n.personal.avatar
            //        ))
            //    .ToListAsync();
        }

    }
}
