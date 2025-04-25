using shared_libraries.Interfaces;
using GrpcServices.Services;
using Microsoft.EntityFrameworkCore;
using friend_service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddGrpc();

builder.Services.AddDbContext<FriendDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("FriendService"),
        ServerVersion.Parse("8.0.42"),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure()
    )
);

var app = builder.Build();


// Configure the HTTP request pipeline.

//app.MapGrpcService<FriendService>();


//builder.WebHost.UseUrls("http://*:8080");

//app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
