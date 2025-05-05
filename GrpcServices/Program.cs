using GrpcServices.Clients;
using GrpcServices.Interfaces;
using GrpcServices.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
var services = builder.Services;

services.AddScoped<IFriendGrpcClient, FriendGrpcClient>();
services.AddScoped<IUserGrpcClient, UserGrpcClient>();

var app = builder.Build();


app.MapGrpcService<GreeterService>();
app.MapGrpcService<NotificationService>();
app.MapGrpcService<UserService>();
app.MapGrpcService<ChatService>();
app.MapGrpcService<FriendService>();

app.Run();
