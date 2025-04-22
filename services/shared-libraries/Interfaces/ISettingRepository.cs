using shared_libraries.Models;
using shared_libraries.DTOs;

namespace shared_libraries.Interfaces
{
    public interface ISettingRepository : IGenericRepository
    {
        Task<Personal?> GetPersonalWithSettingsAndUserAsync(int userId);
        Task<SettingDto?> GetSettings(int userId);
        Task<Settings?> GetUserSettings(int userId);
        Task UpdateChanges(Personal user, SettingDto userInfoDTO);
    }
}
