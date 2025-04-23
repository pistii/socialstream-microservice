using shared_libraries.Models;

namespace shared_libraries.DTOs
{
    public class ChatFileDto : ChatFile
    {
        public byte[]? FileData { get; set; }
    }
}
