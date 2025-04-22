using shared_libraries.Models;
using shared_libraries.Services;

namespace shared_libraries.DTOs
{
    public class ProfilePageDto
    {
        public Personal PersonalInfo { get; set; }
        public ContentDto<PostDto>? Posts { get; set; }
        public List<UserDetailsDto>? Friends { get; set; }
        public UserRelationshipStatus Identity { get; set; }
        public UserSettingsDTO? settings { get; set; }
        public string PublicId { get; set; }
    }
}
