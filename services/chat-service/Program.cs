using chat_service;
using chat_service.Controllers;
using chat_service.Repository;
using chat_service.Storage;
using GrpcServices.Services;
using Microsoft.EntityFrameworkCore;
using shared_libraries.Controllers;
using shared_libraries.Interfaces;
using shared_libraries.Repositories;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.

services.AddGrpc();
services.AddControllers();
services.AddDbContext<ChatDbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("ChatService"),
                    ServerVersion.Parse("8.0.42-mariadb"))); //10.4.6-mariadb


services.AddScoped<IGenericRepository, GenericRepository<ChatDbContext>>();

services.AddScoped<IChatController, ChatController>();

services.AddScoped<IStorageRepository, StorageRepository>();
services.AddScoped<IChatRepository, ChatRepository>();
services.AddSingleton<IChatStorage, ChatStorage>();
services.AddMemoryCache();

services.AddAuthorization();
services.AddAuthentication();

var app = builder.Build();
app.MapGet("/", () => Console.WriteLine("Request received in chat service"));

app.MapGrpcService<ChatService>();


app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
