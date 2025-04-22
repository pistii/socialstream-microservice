using shared_libraries.DTOs;
using shared_libraries.Models;

namespace shared_libraries.Interfaces
{
    public interface IImageRepository
    {
        Task UpdateDatabaseImageUrl(int userId, string url);
    }
}
