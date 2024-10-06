using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyMvcApplication.Models
{
    public class StudentGrade
    {
        [Required]
        [DisplayName("Student Id")]
        public long SGSTUDID { get; set; }
        [Required]
        [DisplayName("Subject Code")]
        public string SGSTUDSUBJCODE { get; set; } = null!;
        [Required]
        [DisplayName("Subject Grade")]
        public Double SGSTUDSUBJGRADE { get; set; }
        [Required]
        [DisplayName("Student Edp Code")]
        public string SGSTUDEDPCODE { get; set; } = null!;
    }
}
