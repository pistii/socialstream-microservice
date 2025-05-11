using GrpcServices.Clients;
using GrpcServices.Interfaces;
using GrpcServices.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
var services = builder.Services;

//Add dependencies
services.AddScoped<IUserGrpcClient, UserGrpcClient>();
services.AddScoped<IFriendGrpcClient, FriendGrpcClient>();
services.AddScoped<INotificationGrpcClient, NotificationGrpcClient>();


var app = builder.Build();

app.MapGrpcService<NotificationService>();
app.MapGrpcService<UserService>();
app.MapGrpcService<ChatService>();
app.MapGrpcService<FriendService>();

app.Run();
