using shared_libraries.Interfaces;
using GrpcServices.Services;
using notification_service.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGrpcService<NotificationService>();
app.MapGrpcService<UserService>();


//builder.WebHost.UseUrls("http://*:8080");

//app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
