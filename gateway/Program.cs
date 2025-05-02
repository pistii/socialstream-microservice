using shared_libraries.Interfaces;
using GrpcServices.Services;
using notification_service.Repository;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

builder.Services.AddControllers();
builder.Services.AddGrpc();

var app = builder.Build();


// Configure the HTTP request pipeline.

app.MapGrpcService<NotificationService>();
app.MapGrpcService<UserService>();
app.MapGrpcService<FriendService>();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
