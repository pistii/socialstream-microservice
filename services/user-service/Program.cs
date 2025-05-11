using GrpcServices.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using shared_libraries;
using shared_libraries.Interfaces;
using user_service;
using user_service.Controllers;
using shared_libraries.Controllers;
using shared_libraries.Controller;
using user_service.Repositories;
using shared_libraries.Repositories;
using Microsoft.Extensions.Options;
using System.Net;
using Microsoft.EntityFrameworkCore.Storage;


namespace user_service
{
    public class Program
    {
        public static void Main(string[] args)
        {
        var builder = WebApplication.CreateBuilder(args);
            var env = builder.Environment.EnvironmentName;
            var services = builder.Services;

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(8080, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http2;
                });
            });

            services.AddGrpc();

            services.AddControllers();
            services.AddDbContext<UserDbContext>(
            options =>
            {
                if (env == "Testing")
                {
                    Console.WriteLine("Running in testing env....");
                    options.UseMySql(
                        builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.Parse("8.0.42-mariadb")
                        );
                }
                else
                {
                    Console.WriteLine("Running in dev env....");

                    options.UseMySql(
                        builder.Configuration.GetConnectionString("DefaultConnection"),
                        ServerVersion.Parse("8.0.42-mariadb"));
                }
            });

            services.AddScoped<IGenericRepository, GenericRepository<UserDbContext>>();
            services.AddScoped<IUserRepository, UserRepository>();

            var app = builder.Build();

            app.MapGrpcService<UserService>();

            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();

        }
    }
}