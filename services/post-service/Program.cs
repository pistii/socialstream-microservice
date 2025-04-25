using shared_libraries.Interfaces;
using GrpcServices.Services;
using Microsoft.EntityFrameworkCore;
using post_service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllers();
services.AddGrpc();

services.AddDbContext<PostDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("PostService"),
        ServerVersion.Parse("8.0.42"),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure()
    )
);

//services.AddScoped<IPostRepository, PostRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.

//app.MapGrpcService<PostService>();


//builder.WebHost.UseUrls("http://*:8080");

//app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
