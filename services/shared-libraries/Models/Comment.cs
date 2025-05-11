using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using shared_libraries.Interfaces.Shared;

namespace shared_libraries.Models
{
    [Table("Comment")]
    public partial class Comment : IHasPublicId
    {
        [Key]
        [Column(TypeName = "int(11)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int commentId { get; set; }
        [JsonIgnore]
        public int PostId { get; set; }
        [JsonIgnore]
        public int FK_AuthorId { get; set; }
        [StringLength(36)]
        public required string publicId { get; set; }
        public DateTime CommentDate { get; set; }
        [StringLength(500)]
        public string? CommentText { get; set; }
        public DateTime? LastModified { get; set; }
        [JsonIgnore]
        public Post? Post { get; set; }
        //public Personal? AuthorPerson { get; set; }
        public virtual ICollection<CommentReaction> CommentReactions { get; set; } = new HashSet<CommentReaction>();
    }
}
