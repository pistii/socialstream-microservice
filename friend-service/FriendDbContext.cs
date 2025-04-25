using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using shared_libraries.Models;

namespace friend_service
{
    public class FriendDbContext : DbContext
    {
        public FriendDbContext(DbContextOptions<FriendDbContext> options)
            : base(options)
        {
        }



        public virtual DbSet<Friend> Friendship { get; set; }
        public virtual DbSet<FriendshipStatus> FriendshipStatus { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.HasOne(x => x.friendship_status)
                    .WithOne(x => x.friendship);
            });

        }
    }
}
