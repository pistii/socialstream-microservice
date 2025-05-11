using shared_libraries.DTOs;
using shared_libraries.Interfaces.Shared;
using shared_libraries.Models;

namespace shared_libraries.Interfaces
{
    public interface IUserRepository : IGenericRepository
    {
        Task<User?> GetUserByEmailOrPassword(string email, string password);
        Task<User?> GetUserByEmailAsync(string email, bool withPersonal = true);
        Task SendActivationEmail(string email, User user);
        Task<bool> CanUserRequestMoreActivatorToday(string email);
        Task<Personal?> GetPersonalWithSettingsAndUserAsync(string userId);
        Task<Personal?> GetPersonalWithSettingsAndUserAsync(int userId);
        Task<IEnumerable<Personal>> GetMessagePartnersById(List<string> partnerIds, string userId);
        Task<User?> GetByPublicId(string publicId);
        Task<User> GetUserByIdForKafka(int userId);
        Task<List<Personal>> GetAllPersonalWithId(List<int> userIds);
        Task<List<User>> GetAllUserWithId(List<int> userIds);
        Task<List<User>> GetAllUserTest();
        Task<List<Personal>> GetAllPersonalTest();
    }
}
