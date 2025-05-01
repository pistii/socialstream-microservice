
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shared_libraries.Models
{
    [Table("userrestriction")]
    public class UserRestriction
    {
        [Key]
        public int UserId { get; set; }
        public int RestrictionId { get; set; }

        public virtual User user { get; set; } = null!;
        public virtual Restriction restriction { get; set; } = null!;
    }
}
