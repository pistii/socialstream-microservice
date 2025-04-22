using chat_service;
using chat_service.Interfaces;
using chat_service.Repository;
using chat_service.Storage;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ChatDbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("ChatService"),
                    ServerVersion.Parse("10.4.20-mariadb"))); //10.4.6-mariadb


builder.Services.AddScoped<IStorageRepository, StorageRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddSingleton<IChatStorage, ChatStorage>();
builder.Services.AddMemoryCache();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
