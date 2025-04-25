using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace shared_libraries.Models
{
    [Table("personalpost")]
    public class PersonalPost
    {
        [Key]
        public int PersonalPostId { get; set; }
        public int AuthorId { get; set; }
        public int PostedToId { get; set; }
        public int PostId { get; set; }
        public virtual Post? Posts { get; set; }
    }
}
