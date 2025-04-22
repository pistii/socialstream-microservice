using shared_libraries.Models;

namespace shared_libraries.Interfaces
{
    public interface IMobileRepository : IGenericRepository
    {
        Task<IEnumerable<ChatRoom>> getChatRooms(int id);

    }
}