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
    }
}
