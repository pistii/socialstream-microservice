using GrpcServices.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using notification_service;
using notification_service.Repository;
using shared_libraries.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<NotificationDBContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("NotificationService"),
                    ServerVersion.Parse("8.0.42-mariadb"))); //10.4.6-mariadb
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

builder.Services.AddGrpc();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ConfigureEndpointDefaults(opt =>
    {
        opt.Protocols = HttpProtocols.Http2;
    });
});


var app = builder.Build();
app.MapGrpcService<NotificationService>();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

app.MapGet("/", () => "gRPC Service is running");
app.UseAuthorization();

app.MapControllers();

app.Run();
