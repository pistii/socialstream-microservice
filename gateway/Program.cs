using shared_libraries.Interfaces;
using GrpcServices.Services;
using notification_service.Repository;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

builder.Services.AddControllers();
builder.Services.AddGrpc();
            services.AddSignalR(options =>
            {
                options.MaximumReceiveMessageSize = 102400000;
                options.EnableDetailedErrors = true;

            });


// Configure the HTTP request pipeline.

app.MapGrpcService<NotificationService>();
app.MapGrpcService<UserService>();
app.MapGrpcService<FriendService>();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();
            app.MapHub<NotificationHub>("/Notification");
            app.MapHub<ChatHub>("/Chat");
app.Run();
