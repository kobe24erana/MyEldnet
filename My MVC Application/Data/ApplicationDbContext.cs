using Microsoft.EntityFrameworkCore;
using MyMvcApplication.Models;

namespace MyMvcApplication.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }    
        public DbSet<Student> Students { get; set; }
        public DbSet<SubjectAndSubjectPreq> SubjectAndSubjectPreqs { get; set; }
        public DbSet<SubjectSched> SubjectScheds { get; set; }
        public DbSet<EnrollmentHeader> EnrollmentHeaderFiles { get; set; }
        public DbSet<EnrollmentDetail> EnrollmentDetails { get; set; }

    }
}
