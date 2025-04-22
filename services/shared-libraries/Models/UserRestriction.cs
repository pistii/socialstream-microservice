
using System.ComponentModel.DataAnnotations;

namespace shared_libraries.Models
{
    public class UserRestriction
    {
        [Key]
        public int UserId { get; set; }
        public int RestrictionId { get; set; }

        public virtual User user { get; set; } = null!;
        public virtual Restriction restriction { get; set; } = null!;
    }
}
