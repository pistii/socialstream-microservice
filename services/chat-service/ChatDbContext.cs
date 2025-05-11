using shared_libraries.Models;
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

        public virtual DbSet<PersonalChatRoom> PersonalChatRoom { get; set; }
        public virtual DbSet<ChatRoom> ChatRoom { get; set; }
        public virtual DbSet<ChatContent> ChatContent { get; set; }
        public virtual DbSet<ChatFile> ChatFile { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonalChatRoom>()
                .HasKey(x => new { x.FK_PersonalId, x.FK_ChatRoomId });

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
