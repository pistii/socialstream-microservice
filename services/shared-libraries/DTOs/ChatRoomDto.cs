namespace shared_libraries.DTOs
{
    public class ChatRoomDto
    {
        public string chatRoomId { get; set; } = null!;
        public string senderId { get; set; } = null!;
        public string receiverId { get; set; } = null!;
        public DateTime? startedDateTime { get; set; }
        public DateTime? endedDateTime { get; set; } = DateTime.Now;
        public virtual ICollection<ChatContentDto> ChatContents { get; set; } = new HashSet<ChatContentDto>();
    }
}
