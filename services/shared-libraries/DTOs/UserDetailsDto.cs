using shared_libraries.Interfaces.Shared;
using shared_libraries.Models;

namespace shared_libraries.DTOs
{
    public class UserDetailsDto
    {
        public UserDetailsDto()
        {
            
        }
        
       
        public UserDetailsDto(Personal personal)
        {
            PublicId = personal.User!.publicId;
            Avatar = personal.avatar;
            FirstName = personal.firstName;
            MiddleName = personal.middleName;
            LastName = personal.lastName;
        }

        public string PublicId { get; set; } = null!;
        public string? Avatar { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
    }

    public class UserDetailsPermitDto : UserDetailsDto, IUserPermit
    {
        public UserDetailsPermitDto()
        {

        }

         public UserDetailsPermitDto(Personal personal)
        {
            this.PublicId = personal.User!.publicId;
            this.Avatar = personal.avatar;
            this.FirstName = personal.firstName;
            this.MiddleName = personal.middleName;
            this.LastName = personal.lastName;
            this.IsActivated = personal.User.isActivated;
            this.IsRestricted = false;
            this.IsOnlineEnabled = personal.User.isOnlineEnabled;
        }

        
        public bool IsActivated { get; set; } = false;
        public bool IsRestricted { get; set; } = false;
        public bool IsOnlineEnabled { get; set; } = true;
    }
}
