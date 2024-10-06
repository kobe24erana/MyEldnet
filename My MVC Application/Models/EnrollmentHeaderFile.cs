using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyMvcApplication.Models
{
    public class EnrollmentHeader
    {
        [Key]
        [Required]
        [DisplayName("Stud ID")]
        public long ENRHSTUDID { get; set; }
        [Required]
        [DisplayName("Date Enroll")]
        public DateTime ENRHSTUDDATEENROLL { get; set; }
        [Required]
        [DisplayName("School Year")]
        public string ENRHSTUDSCHLYR { get; set; } = null!;
        [Required]
        [DisplayName("Encoder")]
        public string ENRHSTUDENCODER { get; set; } = null!;
        [Required]
        [DisplayName("Total Units")]
        public Double ENRHSTUDTOTALUNITS { get; set; }
        [Required]
        [DisplayName("Status")]
        public string ENRHSTUDSTATUS { get; set; } = null!;

    }
}
