using shared_libraries.Models;

namespace shared_libraries.DTOs
{
    public class RegisterForm : User
    {
        public string Password { get; set; } = null!;
    }
}
