using Grpc.Net.Client;
using static GrpcServices.Notification;
using static GrpcServices.User;

namespace GrpcServices.Handlers
{
    public static class ClientConnectionHandler
    {

        public static NotificationClient NotificationClient { get; private set; } = null!;
        public static UserClient UserClient { get; private set; } = null!;

        public static void Initialize(string address)
        {
            var httpHandler = new SocketsHttpHandler
            {
                EnableMultipleHttp2Connections = true
            };


            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress(address, new GrpcChannelOptions
            {
                HttpHandler = httpHandler
            });

            if (address.Contains("notification"))
            {
                var client = new NotificationClient(channel);
                NotificationClient = client;
            }
            else if (address.Contains("user"))

            {
                var client = new UserClient(channel);
                UserClient = client;
            }

        }
    }
}
