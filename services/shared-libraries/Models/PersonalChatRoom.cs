using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace shared_libraries.Models
{
    [Table("personalchatroom")]
    public class PersonalChatRoom
    {
        [Key]
        public int Id { get; set; }
        public int FK_PersonalId { get; set; }
        public int FK_ChatRoomId { get; set; }

    }
}
