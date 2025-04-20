using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace shared_libraries.Models
{
    public partial class Personal
    {
        public Personal()
        {
        }

        public int id { get; set; }

        public string firstName { get; set; } = null!;

        [StringLength(30)]
        public string? middleName { get; set; }
        public string lastName { get; set; } = null!;
        public bool isMale { get; set; }
        [StringLength(70)]
        public string? PlaceOfResidence { get; set; }
        [StringLength(150)]
        public string? avatar { get; set; } = string.Empty;

        [StringLength(15)]
        public string? phoneNumber { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        [StringLength(100)]
        public string? PlaceOfBirth { get; set; }

        [StringLength(60)]
        public string? Profession { get; set; } = string.Empty;

        [StringLength(120)]
        public string? Workplace { get; set; } = string.Empty;
    }
}
