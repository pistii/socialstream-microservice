using chat_service.Models;
using System.ComponentModel.DataAnnotations;

namespace chat_service.DTO
{
    public class ChatDto
    {
        public required string receiverPublicId { get; set; }
        public int receiverId { get; set; }
        [StringLength(800)]
        public string? message { get; set; }
        public Status status { get; set; }
        public FileUploadDto? chatFile { get; set; }
    }

    public class ContentDto<T>
    {
        public ContentDto()
        {
            
        }
        public ContentDto(List<T>? data, 
            List<string> participants, 
            int totalPages, 
            int currentPage, 
            int roomId
            )
        {
            CurrentPage = currentPage;
            RoomId = roomId;
            Participants = participants;
        }

        public List<T>? Data { get; set; }
        public List<string> Participants { get; set; } = null!;
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int RoomId { get; set; }
    }

}
