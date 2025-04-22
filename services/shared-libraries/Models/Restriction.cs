using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace shared_libraries.Models
{
    public class Restriction
    {
        public Restriction()
        {
            UserRestriction = new HashSet<UserRestriction>();
        }
        public int RestrictionId { get; set; }
        public int FK_StatusId { get; set; }
        [StringLength(400)]
        public string Description { get; set; } = string.Empty;
        public DateTime EndDate { get; set; }
        public DateTime HappenedOnDate { get; set; } = DateTime.Now;
        [JsonIgnore]
        public virtual ICollection<UserRestriction> UserRestriction { get; }
        
        public virtual UserStatus? UserStatus { get; set; }
    }

    public class RestrictionDto : Restriction
    {
        public int userId { get; set; }
    }
}
