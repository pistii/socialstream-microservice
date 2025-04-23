using chat_service;
using chat_service.Repository;
using chat_service.Storage;
using Microsoft.EntityFrameworkCore;
using shared_libraries.Interfaces;
using shared_libraries.Repositories;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.

services.AddControllers();
services.AddDbContext<ChatDbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("ChatService"),
                    ServerVersion.Parse("10.4.20-mariadb"))); //10.4.6-mariadb

services.AddScoped<IGenericRepository, GenericRepository<ChatDbContext>>();

services.AddScoped<IStorageRepository, StorageRepository>();
services.AddScoped<IChatRepository, ChatRepository>();
services.AddSingleton<IChatStorage, ChatStorage>();
services.AddMemoryCache();

services.AddAuthorization();
services.AddAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
