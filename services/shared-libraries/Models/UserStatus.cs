﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shared_libraries.Models
{
    public partial class UserStatus
    {
        public UserStatus() 
        {
             restrictions = new List<Restriction>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "int(11)")]
        public int PK_StatusId { get; set; }
        [StringLength(30)]
        public string statusName { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Restriction> restrictions { get; set; }
    }
}
