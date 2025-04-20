using chat_service;
using chat_service.Interfaces;
using chat_service.Repository;
using chat_service.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ChatDbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("ChatService"),
                    ServerVersion.Parse("10.4.20-mariadb"))); //10.4.6-mariadb


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    //var key = builder.Configuration.GetValue<string>("JwtConfig:Key");
    var keyBytes = Encoding.ASCII.GetBytes("MY CUSTOM KEY");
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateLifetime = true,
        ValidateAudience = false,
        ValidateIssuer = true,
        ClockSkew = TimeSpan.Zero
    };

    jwt.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
                (path.StartsWithSegments("/Chat")))
            {
                // Read the token out of the query string
                context.Token = accessToken;


            }
            return Task.CompletedTask;
        }
    };
});

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

await app.RunAsync();
