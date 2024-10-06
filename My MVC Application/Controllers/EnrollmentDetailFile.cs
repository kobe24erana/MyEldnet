using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApplication.Data;

namespace MyMvcApplication.Controllers
{
    public class EnrollmentDetailFileController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;

        public EnrollmentDetailFileController(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public IActionResult Index()
        {
            var EnrollmentDetailFile = _dbcontext.EnrollmentDetails.ToList();
            return View(EnrollmentDetailFile);
        }

        public IActionResult Add()
        {
            return View();
        }

    }
}
