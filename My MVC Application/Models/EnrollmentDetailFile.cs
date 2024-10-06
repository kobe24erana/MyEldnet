using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyMvcApplication.Models
{
    [PrimaryKey(nameof(ENRDSTUDID), nameof(ENRDSTUDEDPCODE))]
    public class EnrollmentDetail
    {
        [Required]
        [DisplayName("Student  Id")]
        public long ENRDSTUDID { get; set; }
        [Required]
        [DisplayName("Student Subject Code")]
        public string ENRDSTUDSUBJCODE { get; set; } = null!;
        [Required]
        [DisplayName("Student EDP Code")]
        public string ENRDSTUDEDPCODE { get; set; } = null!;
        [Required]
        [DisplayName("Student Status")]
        public string ENRDSTUDSTATUS { get; set; } = null!;

    }
}
