using Microsoft.AspNetCore.Http;

namespace shared_libraries.Models.Cloud
{
    public class ImageUpload : FileUpload
    {
        public ImageUpload(string name, string type, IFormFile file) : base(name, type, file)
        {
        }

        public int UserId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ImageType { get; set; } = null!; // profile, social
        public bool IsPublic { get; set; } = true;
        public bool IsArchived { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsModified { get; set; }
    }
}
