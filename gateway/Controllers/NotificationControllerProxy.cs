using Grpc.Net.Client;
using GrpcServices;
using Microsoft.AspNetCore.Mvc;
using shared_libraries.Models;
using grpcClient = GrpcServices;
using shared_libraries.Interfaces;
using shared_libraries.Auth.Helpers;
using static GrpcServices.Notification;
using GrpcServices.Handlers;
namespace gateway.Controllers
{
    [Route("api/notification")]
    [ApiController]
    public class NotificationControllerProxy : BaseController
    {
        
        public NotificationControllerProxy() {
            ClientConnectionHandler.Initialize("http://notification-service:8080");
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreateNotification()
        {
            int authorId = 1;
            var receiverId = 2;
            var author = new Personal()
            {
                id = authorId,
                avatar = ""
            };
            CreateNotification notification = new CreateNotification(authorId, receiverId, NotificationType.FriendRequest);
            //var result = null; //await _notificationRepository.SendNotification(receiverId, author, notification);
            return Ok(); //return Ok(result);
        }

        //[Authorize]
        [HttpGet("getAllNotification")]
        public async Task<IActionResult> GetAllNotification(
            [FromQuery(Name = "currentPage")] int currentPage = 1,
            [FromQuery(Name = "itemPerRequest")] int itemPerRequest = 10)
        {
            //int userId = GetUserId();

            var response = await ClientConnectionHandler.NotificationClient.GetAllNotificationAsync(new NotificationRequest
            {
                PublicId = "1",
                Message = "Üzenet a mikroszervíznek!"
            });

            return Ok(response);

            //if (notifications == null)
            //    return NotFound();

            //var few = _notificationRepository.Paginator<GetNotification>(notifications, currentPage, itemPerRequest);

            //if (notifications.Count > 0)
            //    return Ok(few);

            return NotFound();
        }

        //public async Task SendNotification(int receiverUserId, Personal author, CreateNotification createNotification)
        //{
        //    var notification = new dbModel.Notification(createNotification.AuthorId, "", createNotification.NotificationType,
        //        author.User!.PublicId,
        //        author.avatar ?? "",
        //        createNotification.UserId);
        //    await _notificationRepository.InsertSaveAsync(notification);

        //    dbModel.GetNotification getNotification = new(notification);
        //    await _notificationHub.Clients.User(receiverUserId.ToString())
        //        .ReceiveNotification(receiverUserId, getNotification);
        //}

        [HttpGet("grcp")]
        public async Task Notify()
        {
            Console.WriteLine("Test method init");

            var httpHandler = new SocketsHttpHandler
            {
                EnableMultipleHttp2Connections = true
            };


            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress("http://notification-service:8080", new GrpcChannelOptions
            {
                HttpHandler = httpHandler
            });


            var client = new grpcClient.Notification.NotificationClient(channel);

            var response = client.SendNotification(new NotificationRequest
            {
                PublicId = "abc123",
                Message = "Üzenet a mikroszervíznek!"
            });



        }
    }
}
