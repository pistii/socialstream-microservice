using shared_libraries.Interfaces;
using GrpcServices.Services;
using Microsoft.EntityFrameworkCore;
using friend_service.Repositories;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using shared_libraries.Repositories;


namespace friend_service
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            var env = builder.Environment.EnvironmentName;
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(8080, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http2;
                });
            });

            var services = builder.Services;

            services.AddControllers();
            services.AddGrpc();

            services.AddDbContext<FriendDbContext>(
            options =>
            {
                if (env != "Testing")
                {
                    Console.WriteLine("running friend db in dev env...");
                    options.UseMySql(
                        builder.Configuration.GetConnectionString("DefaultConnection"),
                        ServerVersion.Parse("8.0.42")
                    );
                }
                else
                {
                    Console.WriteLine("running friend db in testing env...");
                    options.UseMySql(
                       builder.Configuration.GetConnectionString("DefaultConnection"),
                       ServerVersion.Parse("8.0.42")
                   );
                }
            }
            );
            

            services.AddScoped<IGenericRepository, GenericRepository<FriendDbContext>>();
            services.AddScoped<IFriendRepository, FriendRepository>();

            var app = builder.Build();

            app.MapGrpcService<FriendService>();

            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();

        }
    }
}
