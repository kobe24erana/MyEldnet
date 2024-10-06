using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApplication.Data;
using MyMvcApplication.Models;

namespace MyMvcApplication.Controllers
{
    public class SubjectPreqController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        public SubjectPreqController(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public IActionResult Index()
        {
            var subjectpreq = _dbcontext.SubjectAndSubjectPreqs.ToList();
            var filteredSubjectPreq = subjectpreq.Where(item => !string.IsNullOrEmpty(item.SUBJPRECODE)).ToList();

            return View(filteredSubjectPreq);
        }
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Search(string searchString)
        {
            var subjectpreq = _dbcontext.SubjectAndSubjectPreqs.Where(s => !string.IsNullOrEmpty(s.SUBJPRECODE));

            if (!string.IsNullOrEmpty(searchString))
            {
                subjectpreq = subjectpreq.Where(s => s.SUBJCODE.Contains(searchString));
            }

            return View("Index", subjectpreq.ToList());
        }

        [HttpPost]
        public IActionResult Add(SubjectAndSubjectPreq addSubjectPreqRequest)
        {
            return RedirectToAction("Index");
        }

        //Edit Action
        public IActionResult Edit(string id, string courseCode)
        {
            if (id == null || courseCode == null)
            {
                return NotFound();
            }

            var subject = _dbcontext.SubjectAndSubjectPreqs.FirstOrDefault(m => m.SUBJCODE == id && m.SUBJCOURSECODE == courseCode);

            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SubjectAndSubjectPreq editedSubject)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingSubject = _dbcontext.SubjectAndSubjectPreqs.Find(editedSubject.SUBJCODE, editedSubject.SUBJCOURSECODE);

                    if (existingSubject == null)
                    {
                        return NotFound();
                    }

                    existingSubject.SUBJPRECODE = editedSubject.SUBJPRECODE;
                    existingSubject.SUBJCATEGORY = editedSubject.SUBJCATEGORY;
                    

                    _dbcontext.Update(existingSubject);
                    _dbcontext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(editedSubject.SUBJCODE, editedSubject.SUBJCOURSECODE))
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

            return View(editedSubject);
        }

        public IActionResult Delete(string id, string courseCode)
        {
            if (id == null || courseCode == null)
            {
                return NotFound();
            }

            var subject = _dbcontext.SubjectAndSubjectPreqs.FirstOrDefault(m => m.SUBJCODE == id && m.SUBJCOURSECODE == courseCode);

            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id, string courseCode)
        {
            var subject = _dbcontext.SubjectAndSubjectPreqs.Find(id, courseCode);

            if (subject == null)
            {
                return NotFound();
            }

            _dbcontext.SubjectAndSubjectPreqs.Remove(subject);
            _dbcontext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(string id, string courseCode)
        {
            return _dbcontext.SubjectAndSubjectPreqs.Any(e => e.SUBJCODE == id && e.SUBJCOURSECODE == courseCode);
        }

    }
}
