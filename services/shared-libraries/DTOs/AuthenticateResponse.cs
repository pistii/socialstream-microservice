using Newtonsoft.Json;
using shared_libraries.Models;

namespace shared_libraries.DTOs
{
    public class AuthenticateResponse
    {
        public AuthenticateResponse(Personal personal, string token)
        {
            this.UserDetails = new UserDetailsPermitDto(personal);
            this.Token = token;
        }


        [JsonIgnore]
        public Personal? Personal { get; set; }
        public UserDetailsPermitDto UserDetails { get; set; }
        public string Token { get; set; }
    }
}
