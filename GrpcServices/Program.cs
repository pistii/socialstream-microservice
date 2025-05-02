using GrpcServices.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
var services = builder.Services;

var app = builder.Build();


app.MapGrpcService<GreeterService>();
app.MapGrpcService<NotificationService>();
app.MapGrpcService<UserService>();
app.MapGrpcService<ChatService>();
app.MapGrpcService<FriendService>();



app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
