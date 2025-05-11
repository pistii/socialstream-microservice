using GrpcServices.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using notification_service.Repository;
using shared_libraries.Interfaces;
using shared_libraries.Repositories;


namespace notification_service
{
    public class Program
    {
        protected static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            var env = builder.Environment.EnvironmentName;

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<NotificationDBContext>(
            options => {
                if (env == "Testing")
                {
                    Console.WriteLine("running notification db in testing env...");

                    options.UseMySql(
                        builder.Configuration.GetConnectionString("DefaultConnection"),
                        ServerVersion.Parse("8.0.42")
                    );
                }
                else
                {
                    Console.WriteLine("running notification db in dev env...");
                    options.UseMySql(
                            builder.Configuration.GetConnectionString("DefaultConnection"),
                            ServerVersion.Parse("8.0.42-mariadb"));
                }
            });
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<IGenericRepository, GenericRepository<NotificationDBContext>>();

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
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}