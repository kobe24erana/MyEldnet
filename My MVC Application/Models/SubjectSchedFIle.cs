using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyMvcApplication.Models
{
    public class SubjectSched
    {
        [Key]      
        [Required(ErrorMessage = "Please enter the EDP Code.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "EDP Code must be numeric.")]
        public string? EDPCODE { get; set; }

        [Required]
        [DisplayName("Subject Code")]
        public string SUBJCODE { get; set; } = null!;
        [Required]
        [DisplayName("StartTime")]
        public DateTime STARTTIME { get; set; }
        [Required]
        [DisplayName("Endtime")]
        public DateTime ENDTIME { get; set; } 
        [Required]
        [DisplayName("Days")]
        public string DAYS { get; set; } = null!;
        [Required]
        [DisplayName("Room")]
        public string ROOM { get; set; } = null!;
        [Required]
        [DisplayName("Maxsize")]
        public int MAXSIZE { get; set; }
        [Required]
        [DisplayName("Classsize")]
        public string CLASSSIZE { get; set; } = null!;
        [Required]
        [DisplayName("Status")]
        public string STATUS { get; set; } = null!;
        [Required]
        [DisplayName("Fxm")]
        public string FXM { get; set; } = null!;
        [Required]
        [DisplayName("Section")]
        public string SECTION { get; set; } = null!;
        [Required]
        [DisplayName("SchoolYear")]
        public int SCHOOLYEAR { get; set; }
    }
}
