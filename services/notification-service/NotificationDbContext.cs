using Microsoft.EntityFrameworkCore;
using shared_libraries.Models;

namespace notification_service
{
    public partial class NotificationDBContext : DbContext
    {
        public NotificationDBContext()
        {
        }

        public NotificationDBContext(DbContextOptions<NotificationDBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<UserNotification> UserNotification { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserNotification>()
                .HasOne(un => un.notification)
                .WithMany(p => p.UserNotification)
                .HasForeignKey(un => un.NotificationId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.NotificationType)
                         .HasConversion(
                         c => c.ToString(),
                         type => (NotificationType)Enum.Parse(typeof(NotificationType), type));
            });
        }
    }
}
