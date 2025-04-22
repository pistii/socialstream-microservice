﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace shared_libraries.Models
{
    [MetadataType(typeof(Personal))]
    public partial class Relationship
    {
        public Relationship()
        {
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int? relationshipID { get; set; }

        [Column(TypeName = "int(11)")]
        public int? typeID { get; set; }

        [ForeignKey("relationshipID")]
        [InverseProperty("Relationships")]
        [JsonIgnore]
        public virtual Personal? relationship { get; set; }
        
        [JsonIgnore]
        [InverseProperty("RelationshipTp")]
        public virtual ICollection<RelationshipType>? RelationshipTypes { get; set; } = new HashSet<RelationshipType>();

    }
}