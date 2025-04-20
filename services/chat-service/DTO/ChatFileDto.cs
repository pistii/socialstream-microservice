using chat_service.Models;

namespace chat_service.DTO
{
    public class ChatFileDto : ChatFile
    {
        public byte[]? FileData { get; set; }
    }
}
