using Microsoft.EntityFrameworkCore;
using shared_libraries.Models;

namespace user_service
{

    public partial class UserDbContext : DbContext
    {
        public UserDbContext()
        {
        }

        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Personal> Personal { get; set; }
        public virtual DbSet<UserRestriction> UserRestriction { get; set; }
        public virtual DbSet<Restriction> Restriction { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }


        protected override async void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                "server=userDb;user=root;password=jelszo;database=user-service;port=3306",
                ServerVersion.Parse("8.0.42"),
                mySqlOptions => mySqlOptions.EnableRetryOnFailure());
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(p => p.personal)
                .WithOne(p => p.User)
                .IsRequired();

                entity.HasMany(p => p.Studies)
                .WithOne(u => u.user)
                .HasForeignKey(p => p.FK_UserId);
            });

            modelBuilder.Entity<Restriction>(entity =>
            {
                entity.HasMany(s => s.UserRestriction)
               .WithOne(g => g.restriction)
               .HasForeignKey(s => s.RestrictionId);

                entity.HasOne(s => s.UserStatus)
                  .WithMany(g => g.restrictions)
                  .HasForeignKey(s => s.FK_StatusId);
            });

            modelBuilder.Entity<UserRestriction>(entity =>
            {
                entity.HasOne(p => p.user)
                .WithMany(p => p.UserRestriction)
                .HasForeignKey(p => p.UserId);

                entity.HasOne(p => p.restriction)
               .WithMany(p => p.UserRestriction)
               .HasForeignKey(p => p.RestrictionId);

                entity.HasKey(x => new { x.UserId, x.RestrictionId });
            });


            modelBuilder.Entity<Personal>(entity =>
            {
                entity.HasMany(c => c.Relationships)
                    .WithOne(x => x.relationship)
                    .HasForeignKey(c => c.relationshipID);


                entity.HasMany(_ => _.PersonalChatRooms)
                    .WithOne(_ => _.PersonalRoom)
                    .HasForeignKey(_ => _.FK_PersonalId);

                entity.HasOne(p => p.Settings)
                    .WithOne(p => p.personal)
                    .HasForeignKey<Settings>(s => s.FK_UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
