using shared_libraries.DTOs;
using shared_libraries.Models;

namespace shared_libraries.Interfaces
{
    public interface IChatRepository : IGenericRepository
    {
        Task<ChatRoom?> GetChatRoomById(int id);
        Task<List<ChatRoomDto>> GetAllChatRoomAsQuery(string authorId, int userId);
        Task<ChatRoom?> ChatRoomExists(int senderId, int receiverId);
        List<int> GetChatPartenterIds(int userId);
        Task<ChatRoom> CreateChatRoom(int senderId, int receiverId, string receiverPublicId);
        List<ChatContent> GetSortedChatContent(int roomId);
        Task<ChatRoom?> GetChatRoomByUser(int user1, int user2);
        Task<object> AddChatFile(ChatFile fileUpload);
        Task<string> GetChatFileTypeAsync(string token);
        //Task<List<ChatContentDto>> GetChatFile(IEnumerable<ChatContentDto> returnValue);
    }
}
