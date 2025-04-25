using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using shared_libraries.Models;

namespace post_service
{
    public class PostDbContext : DbContext
    {
        public PostDbContext(DbContextOptions<PostDbContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<PersonalPost> PersonalPost { get; set; }
        public virtual DbSet<Comment>? Comment { get; set; }
        public virtual DbSet<CommentReaction>? CommentReaction { get; set; }
        public virtual DbSet<PostReaction> PostReaction { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.HasMany(r => r.PostReactions)
                    .WithOne(p => p.post)
                    .HasForeignKey(p => p.PostId);

                entity.HasOne(p => p.MediaContent)
                .WithOne(p => p.Post)
                .HasForeignKey<MediaContent>(p => p.FK_PostId);
            });

            modelBuilder.Entity<PostReaction>(entity =>
            {
                entity.HasKey(p => p.Pk_Id);

                entity.HasMany(r => r.ReactionTypes)
                .WithOne(p => p.PostReaction)
                .HasForeignKey(p => p.Id);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(p => p.commentId);

                entity.HasOne(_ => _.Post)
                    .WithMany(x => x.PostComments)
                    .HasForeignKey(i => i.PostId);

                entity.HasMany(p => p.CommentReactions)
                    .WithOne(p => p.Comment)
                    .HasForeignKey(x => x.FK_CommentId);
            });

            modelBuilder.Entity<PersonalPost>(entity =>
            {
                entity.HasOne(p => p.Posts)
               .WithMany(p => p.PersonalPosts)
               .HasForeignKey(p => p.PostId);

                entity.HasKey(x => new { x.PersonalPostId });
            });
        }
    }
}
