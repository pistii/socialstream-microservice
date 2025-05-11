using shared_libraries.Models;
using System.Text.Json.Serialization;

namespace shared_libraries.DTOs
{
    

    public class ChatContentDto
    {
        public ChatContentDto()
        {
            
        }
        public ChatContentDto(string authorId, bool isAuthor, ChatContent chatContent)
        {
            PublicId = chatContent.publicId;
            AuthorId = authorId;
            Message = chatContent.message;
            chatRoomId = chatContent.chatContentId;
            IsAuthor = isAuthor;
            SentDate = chatContent.sentDate;
            status = chatContent.status;
        }

        public string PublicId { get; set; } = null!;
        public string AuthorId { get; set; } = null!;
        public string ReceiverId { get; set; } = null!;
        public string? Message { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public int chatRoomId { get; set; }
        public bool IsAuthor { get; set; }
        public DateTime SentDate { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status status { get; set; }
        public ChatFileDto? ChatFile { get; set; }
    }

}
