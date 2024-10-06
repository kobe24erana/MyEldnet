using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApplication.Data;
using MyMvcApplication.Models;

namespace MyMvcApplication.Controllers
{
    public class SubjectSchedController : Controller
    {

        private readonly ApplicationDbContext _dbContext;
        public SubjectSchedController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var subjsched = _dbContext.SubjectScheds.ToList();
            return View(subjsched);
        }


        public IActionResult Search(string searchString)
        {
            var subjectsched = from s in _dbContext.SubjectScheds
                               select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                subjectsched = subjectsched.Where(s => s.EDPCODE.Contains(searchString) || s.SUBJCODE.Contains(searchString));
            }

            return View("Index", subjectsched.ToList());
        }

        //Edit
        public IActionResult Edit(string id)
        {
            // Retrieve the SubjectSched by EDP Code
            var subjectSched = _dbContext.SubjectScheds.Find(id);

            if (subjectSched == null)
            {
                return NotFound();
            }

            return View(subjectSched);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, SubjectSched updatedSubjectSched)
        {
            Console.WriteLine("Edit action is being executed.");

            if (updatedSubjectSched == null || id != updatedSubjectSched.EDPCODE)
            {
                Console.WriteLine("Invalid data or ID mismatch.");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Console.WriteLine("ModelState is valid.");

                // Update the SubjectSched in the database
                _dbContext.Entry(updatedSubjectSched).State = EntityState.Modified;
                _dbContext.SaveChanges();

                Console.WriteLine("Changes saved successfully.");

                return RedirectToAction("Index");
            }

            Console.WriteLine("ModelState is not valid.");

            return View(updatedSubjectSched);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var subjectsched = _dbContext.SubjectScheds.Find(id);

            if (subjectsched == null)
            {
                return NotFound();
            }

            _dbContext.SubjectScheds.Remove(subjectsched);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(string id)
        {
            return _dbContext.SubjectAndSubjectPreqs.Any(e => e.SUBJCODE == id);
        }


        public IActionResult Add()
        {
            return View();
        }
    
        private (bool Exists, string SubjCode) CheckSubjectCode(string subjCode)
        {
            var result = _dbContext.SubjectAndSubjectPreqs.Any(s => s.SUBJCODE == subjCode);
            return (result, subjCode);
        }



        [HttpPost]
        public ActionResult Add(SubjectSched addSubjectSchedRequest)
        {
            if (string.IsNullOrEmpty(addSubjectSchedRequest.EDPCODE) ||

            string.IsNullOrEmpty(addSubjectSchedRequest.SUBJCODE) ||

            string.IsNullOrEmpty(addSubjectSchedRequest.STARTTIME.ToString()) ||

            string.IsNullOrEmpty(addSubjectSchedRequest.ENDTIME.ToString()) ||

            string.IsNullOrEmpty(addSubjectSchedRequest.FXM) ||

            string.IsNullOrEmpty(addSubjectSchedRequest.ROOM) ||

            string.IsNullOrEmpty(addSubjectSchedRequest.DAYS) ||

            string.IsNullOrEmpty(addSubjectSchedRequest.SECTION) ||

            string.IsNullOrEmpty(addSubjectSchedRequest.SCHOOLYEAR.ToString()))

            {
                TempData["MissingFieldsError"] = "Please fill out all the required fields.";
                return View(addSubjectSchedRequest);
            }


            if (_dbContext.SubjectScheds.Any(s => s.EDPCODE == addSubjectSchedRequest.EDPCODE))
            {
                TempData["DuplicateError"] = "Duplicate Error: The EDP Code already exists. Cannot Proceed.";
                return View(addSubjectSchedRequest);
            }

            var subjCodeExistsResult = CheckSubjectCode(addSubjectSchedRequest.SUBJCODE);
            if (!subjCodeExistsResult.Exists)
            {
                TempData["SubjectCodeError"] = $"Subject Code {subjCodeExistsResult.SubjCode} does not exist.";
                return View(addSubjectSchedRequest);
            }

            if (HasScheduleConflict(addSubjectSchedRequest))
            {
                TempData["ConflictError"] = "Schedule conflicts with an existing schedule. Cannot Proceed.";
                return View(addSubjectSchedRequest);
            }

            var subjsched = new SubjectSched
            {
                EDPCODE = addSubjectSchedRequest.EDPCODE,
                SUBJCODE = addSubjectSchedRequest.SUBJCODE,
                STARTTIME = addSubjectSchedRequest.STARTTIME,
                ENDTIME = addSubjectSchedRequest.ENDTIME,
                DAYS = addSubjectSchedRequest.DAYS,
                ROOM = addSubjectSchedRequest.ROOM,
                MAXSIZE = 1,
                CLASSSIZE = "25",
                STATUS = "AC",
                FXM = addSubjectSchedRequest.FXM,
                SECTION = addSubjectSchedRequest.SECTION,
                SCHOOLYEAR = addSubjectSchedRequest.SCHOOLYEAR
            };

            _dbContext.SubjectScheds.Add(subjsched);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        private bool HasScheduleConflict(SubjectSched newSubjectSched)
        {
            return _dbContext.SubjectScheds.Any(s =>
                s.DAYS == newSubjectSched.DAYS &&
                s.ROOM == newSubjectSched.ROOM &&
                ((newSubjectSched.STARTTIME >= s.STARTTIME && newSubjectSched.STARTTIME < s.ENDTIME) ||
                 (newSubjectSched.ENDTIME > s.STARTTIME && newSubjectSched.ENDTIME <= s.ENDTIME) ||
                 (newSubjectSched.STARTTIME <= s.STARTTIME && newSubjectSched.ENDTIME >= s.ENDTIME)));
        }


    }
}


