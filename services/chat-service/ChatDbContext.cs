using chat_service.Models;
using Microsoft.EntityFrameworkCore;

namespace chat_service
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext()
        {
        }

        public ChatDbContext(DbContextOptions<ChatDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChatRoom> ChatRoom { get; set; }
        public virtual DbSet<ChatContent> ChatContent { get; set; }
        public virtual DbSet<ChatFile> ChatFile { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            //    if (string.IsNullOrEmpty(connectionString))
            //    {
            //        throw new InvalidOperationException("DB_CONNECTION_STRING is not set.");
            //    }

            //    optionsBuilder.UseMySql(connectionString, ServerVersion.Parse("10.4.20-mariadb"));
            //}

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                "server=chatDb;user=root;password=jelszo;database=chat-service;port=3306",
                ServerVersion.Parse("8.0.42"),
                mySqlOptions => mySqlOptions.EnableRetryOnFailure());
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ChatRoom>(entity =>
            {
                entity.HasMany(x => x.ChatContents)
                    .WithOne(x => x.ChatRooms)
                    .HasForeignKey(x => x.chatContentId);
            });

            modelBuilder.Entity<ChatContent>(entity =>
            {
                entity.Property(e => e.status)
                    .HasConversion(
                    c => c.ToString(),
                    v => (Status)Enum.Parse(typeof(Status), v));

                entity.HasIndex(e => e.chatContentId).IsUnique();


                entity.HasOne(e => e.ChatFile)
                    .WithOne(e => e.ChatContent)
                    .HasForeignKey<ChatFile>(e => e.ChatContentId);
            });

        }
    }
}
