using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace chat_service.Models
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