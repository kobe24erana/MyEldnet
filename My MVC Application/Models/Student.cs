using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMvcApplication.Models
{
    public class Student
    {
        [Key]
        [Required(ErrorMessage = "ID Number is required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid Student ID Number. Please enter numbers only.")]
        public string? STUDID { get; set; }

        [DisplayName("Last")]
        public string? STUDLNAME { get; set; }

        [DisplayName("First")]
        public string? STUDFNAME { get; set; }

        [DisplayName("Middle")]
        public string? STUDMNAME { get; set; }

        [DisplayName("Course")]
        public string? STUDCOURSE { get; set; }

        [DisplayName("Year")]
        public int? STUDYEAR { get; set; }

        [DisplayName("Remarks")]
        public string? STUDREMARKS { get; set; }

        [DisplayName("Status")]
        public string? STUDSTATUS { get; set; }

        [Display(Name = "Student Image")]
        [NotMapped] // Add this attribute to indicate that this property is not mapped to the database
        public IFormFile? StudentImage { get; set; }

        public string? ImagePath { get; set; }

    }
}
