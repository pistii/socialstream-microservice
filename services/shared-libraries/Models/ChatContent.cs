using Newtonsoft.Json;
using shared_libraries.Interfaces.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace shared_libraries.Models
{
    [Table("chatcontent")]
    public class ChatContent : IHasPublicId
    {
        public ChatContent()
        {
            sentDate = DateTime.Now;
            publicId = Guid.NewGuid().ToString("N");
        }

        [Key]
        [ForeignKey("ChatFile")]
        public int MessageId { get; set; }
        public string publicId { get; set; }
        public int AuthorId { get; set; }
        public int chatContentId { get; set; }
        [StringLength(800)]
        public string? message { get; set; } = null;
        public DateTime sentDate { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public Status status { get; set; }
        [JsonIgnore]
        public virtual ChatRoom? ChatRooms { get; set; }
        [JsonIgnore]
        public virtual ChatFile? ChatFile { get; set; }
        
    }

    public enum Status
    {
        Read = 0,
        Sent = 1,
        Delivered = 2
    }
}
