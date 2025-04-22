using System.ComponentModel.DataAnnotations.Schema;

namespace shared_libraries.Models
{
    public class BlockedPerson
    {
        [Column(TypeName = "int(11)")]
        public int BlockedPersonId { get; set; }
    }
}
