using System.ComponentModel.DataAnnotations.Schema;

namespace shared_libraries.Models
{
    public class FollowPerson
    {

        [Column(TypeName = "int(11)")]
        public int FollowedPersonId { get; set; }
    }
}
