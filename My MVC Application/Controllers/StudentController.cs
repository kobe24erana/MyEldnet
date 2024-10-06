using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApplication.Models;
using MyMvcApplication.Data;


namespace MyMvcApplication.Controllers
{
    public class StudentController : Controller
    {

        private readonly ApplicationDbContext _dbcontext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public StudentController(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbcontext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

      

        public IActionResult Search(string searchString)
        {

            var students = from s in _dbcontext.Students
                           select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.STUDID.Contains(searchString) || s.STUDLNAME.Contains(searchString));
            }

            return View("Index", students.ToList());
        }

        public IActionResult Index()
        {
            var students = _dbcontext.Students.ToList();
            return View(students);
        }

		

		public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _dbcontext.Students.FirstOrDefault(s => s.STUDID == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("STUDID,STUDLNAME,STUDFNAME,STUDMNAME,STUDCOURSE,STUDYEAR,STUDREMARKS,STUDSTATUS")] Student student)
        {
            if (id != student.STUDID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _dbcontext.Update(student);
                    _dbcontext.SaveChanges();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.STUDID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }



        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _dbcontext.Students.FirstOrDefault(m => m.STUDID == id);

            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var student = _dbcontext.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            _dbcontext.Students.Remove(student);
            _dbcontext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        private bool StudentExists(string id)
        {
            return _dbcontext.Students.Any(e => e.STUDID == id);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Student addStudentRequest)
        {
            if (string.IsNullOrEmpty(addStudentRequest.STUDID) ||
       
            string.IsNullOrEmpty(addStudentRequest.STUDLNAME) ||

            string.IsNullOrEmpty(addStudentRequest.STUDFNAME) ||

            string.IsNullOrEmpty(addStudentRequest.STUDMNAME)||

            string.IsNullOrEmpty(addStudentRequest.STUDCOURSE) ||

            string.IsNullOrEmpty(addStudentRequest.STUDYEAR.ToString()))      
          

            {
                TempData["MissingFieldsError"] = "Please fill out all the required fields.";
                return View(addStudentRequest);
            }


            if (!ModelState.IsValid)
            {
                TempData["DuplicateError"] = "Invalid Student Id Number";
                return View(addStudentRequest);
            }

            else if (_dbcontext.Students.Any(s => s.STUDID == addStudentRequest.STUDID))
            {
                TempData["DuplicateError"] = "Duplicate Error: The ID Number already exists. Cannot Proceed.";
                return View(addStudentRequest);
            }

            var student = new Student()
            {
                STUDID = addStudentRequest.STUDID,
                STUDLNAME = addStudentRequest.STUDLNAME,
                STUDFNAME = addStudentRequest.STUDFNAME,
                STUDMNAME = addStudentRequest.STUDMNAME,
                STUDCOURSE = addStudentRequest.STUDCOURSE,
                STUDYEAR = addStudentRequest.STUDYEAR,
                STUDREMARKS = addStudentRequest.STUDREMARKS,
                STUDSTATUS = "AC"

            };

            _dbcontext.Students.Add(student);
            _dbcontext.SaveChanges();
           

            return RedirectToAction("Index");
        }

        
    }
}