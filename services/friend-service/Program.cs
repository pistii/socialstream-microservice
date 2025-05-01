using shared_libraries.Interfaces;
using GrpcServices.Services;
using Microsoft.EntityFrameworkCore;
using friend_service;
using shared_libraries.Controllers;
using shared_libraries.Kafka;
using friend_service.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllers();
services.AddGrpc();

services.AddDbContext<FriendDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("FriendService"),
        ServerVersion.Parse("8.0.42"),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure()
    )
);

services.AddScoped<IFriendRepository, FriendRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.

//app.MapGrpcService<FriendService>();


//builder.WebHost.UseUrls("http://*:8080");

//app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
