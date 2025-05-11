using shared_libraries.Interfaces;
using GrpcServices.Services;
using GrpcServices.Clients;
using GrpcServices.Interfaces;
using GrpcServices.Protos;
using Serilog;
using GrpcServices;
using gateway.Realtime;


namespace gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
            var configuration = builder.Configuration;
            services.AddScoped<IUserGrpcClient, UserGrpcClient>();
            services.AddScoped<IFriendGrpcClient, FriendGrpcClient>();
            services.AddScoped<INotificationGrpcClient, NotificationGrpcClient>();

            services.AddSignalR(options =>
            {
                options.MaximumReceiveMessageSize = 102400000;
                options.EnableDetailedErrors = true;

            });
            //Grpc configuration
            services.AddGrpc();
            services.AddGrpcClient<GrpcNotification.GrpcNotificationClient>(o =>
            {
                o.Address = new Uri(configuration["GrpcServices:NotificationServiceUrl"]);
            });
            services.AddGrpcClient<User.UserClient>(o =>
            {
                o.Address = new Uri(configuration["GrpcServices:UserServiceUrl"]);
            });
            services.AddGrpcClient<Friend.FriendClient>(o =>
            {
                o.Address = new Uri(configuration["GrpcServices:FriendServiceUrl"]);
            });

            services.AddControllers();

            var app = builder.Build();

app.MapGrpcService<NotificationService>();
app.MapGrpcService<UserService>();
app.MapGrpcService<FriendService>();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
            app.MapHub<NotificationHub>("/Notification");
            app.MapHub<ChatHub>("/Chat");
app.Run();
        }
    }
}