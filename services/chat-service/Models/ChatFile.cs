using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace chat_service.Models
{
    public class ChatFile
    {
        [Key]
        public int FileId { get; set; }
        public int ChatContentId { get; set; }
        [StringLength(30)]
        public string FileType { get; set; } = null!;
        [StringLength(100)]
        public string FileToken { get; set; } = null!;
        public int FileSize { get; set; }
        [JsonIgnore]
        public virtual ChatContent? ChatContent { get; set; }
    }
}