using chat_service.Interfaces.Shared;

namespace chat_service.Models
{
    public partial class User : IHasPublicId
    {

        public User()
        {
        }

        public int userID { get; set; }

        public string email { get; set; }
        public string? SecondaryEmailAddress { get; set; }
        public string? password { get; set; }
        public DateTime? registrationDate { get; set; } = DateTime.Now;
        public virtual bool isActivated { get; set; } = false;
        public string PublicId { get; set; }
        public DateTime LastOnline { get; set; }
        public bool isOnlineEnabled { get; set; } = true;
        public virtual Personal? personal { get; set; }


        public User(User user)
        {
            this.email = user.email;
            this.personal = user.personal;
            this.SecondaryEmailAddress = user.SecondaryEmailAddress;
            this.userID = user.userID;
            this.isOnlineEnabled = user.isOnlineEnabled;
            this.LastOnline = user.LastOnline;
        }
    }
}
