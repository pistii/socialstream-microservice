using GrpcServices.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using shared_libraries;
//using user_service.Repositories;
using shared_libraries.Interfaces;
using user_service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<UserDbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("UserService"),
                    ServerVersion.Parse("8.0.42-mariadb")));

//builder.Services.AddScoped<Interface, Repository>();

builder.Services.AddGrpc();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ConfigureEndpointDefaults(opt =>
    {
        opt.Protocols = HttpProtocols.Http2;
    });
});


var app = builder.Build();
app.MapGrpcService<UserService>();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
