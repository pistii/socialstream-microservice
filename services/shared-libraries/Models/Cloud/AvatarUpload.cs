using Microsoft.AspNetCore.Http;

namespace shared_libraries.Models.Cloud
{
    public class AvatarUpload : FileUpload
    {
        public AvatarUpload(string name, string type, IFormFile file) : base(name, type, file)
        {
        }

        public AvatarUpload() { }
    }
}
