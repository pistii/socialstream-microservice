using gateway.DTOs;
using gateway.Interfaces;
using Grpc.Net.Client;
using GrpcServices;
using Microsoft.AspNetCore.Mvc;
using shared_libraries.Auth.Helpers;

namespace gateway.Controllers
{
    [ApiController]
    public class NotificationControllerProxy : ControllerBase //: BaseController
    {
        //private readonly INotificationRepository _notificationRepository;

        public NotificationControllerProxy()
          //INotificationRepository notificationRepository)
        {
            //_notificationRepository = notificationRepository;

        }
        //[Authorize]
        //[HttpGet("getAll")]
        //public async Task<IActionResult> GetAll(
        //     [FromQuery(Name = "currentPage")] int currentPage = 1,
        //     [FromQuery(Name = "itemPerRequest")] int itemPerRequest = 10)
        //{
        //    int userId = GetUserId();

        //    var notifications = await _notificationRepository.GetAllNotifications(userId);

        //    if (notifications == null)
        //        return NotFound();

        //    var few = _notificationRepository.Paginator<GetNotification>(notifications, currentPage, itemPerRequest);

        //    if (notifications.Count > 0)
        //        return Ok(few);

        //    return NotFound();
        //}

        [HttpGet("isrunning")]
        public IActionResult Test()
        {
            return Ok();
        }

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


            var client = new Notification.NotificationClient(channel);

            var response = client.SendNotification(new NotificationRequest
            {
                PublicId = "abc123",
                Message = "Üzenet a mikroszervíznek!"
            });



        }
    }
}
