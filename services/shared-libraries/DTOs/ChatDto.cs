using shared_libraries.DTOs;
using shared_libraries.Models;
using System.ComponentModel.DataAnnotations;

namespace shared_libraries.DTOs
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

    public class ChatRoomWithContentDto<T>
    {
        public ChatRoomWithContentDto()
        {

        }
        public ChatRoomWithContentDto(List<T>? data,
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
