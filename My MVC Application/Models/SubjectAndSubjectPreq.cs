using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace MyMvcApplication.Models
{
    [PrimaryKey(nameof(SUBJCODE), nameof(SUBJCOURSECODE))]
    public class SubjectAndSubjectPreq
    {
        [DisplayName("Subject Code")]
        [Required]
        public string? SUBJCODE { get; set; }
        [DisplayName("Description")]

        public string? SUBJDESC { get; set; }
        [DisplayName("Units")]

        public int? SUBJUNITS { get; set; }
        [DisplayName("Offering")]

        public int? SUBJREGOFRNG { get; set; }
        [DisplayName("Category")]

        public string? SFSUBJCATEGORY { get; set; }
        [DisplayName("Status")]

        public string? SUBJSTATUS { get; set; }
        [DisplayName("Course Code")]

        public string? SUBJCOURSECODE { get; set; }
        [DisplayName("Curriculum")]

        public string? SUBJCURRCODE { get; set; }

        //PREQ AREA
        [DisplayName("Prequisite")]
        public string? SUBJPRECODE { get; set; } = null!;

        [DisplayName("Category")]
        public string? SUBJCATEGORY { get; set; } = null!;


    }

}
