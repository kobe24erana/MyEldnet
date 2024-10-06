using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MyMvcApplication.Data;
using MyMvcApplication.Models;
using System;

namespace MyMvcApplication.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        public SubjectController(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public IActionResult Search(string searchString)
        {
            var subject = from s in _dbcontext.SubjectAndSubjectPreqs
                          select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                subject = subject.Where(s => s.SUBJCODE.Contains(searchString));
            }

            return View("Index", subject.ToList());
        }

        public JsonResult CheckIfSubjectCodeExists(string subjCode)
        {
            var exists = _dbcontext.SubjectAndSubjectPreqs.Any(s => s.SUBJCODE == subjCode);
            return Json(new { Exists = exists, SubjCode = subjCode });
        }

        public IActionResult Index()
        {
            var subjects = _dbcontext.SubjectAndSubjectPreqs.ToList();
            return View(subjects);
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

                    existingSubject.SUBJDESC = editedSubject.SUBJDESC;
                    existingSubject.SUBJUNITS = editedSubject.SUBJUNITS;
                    existingSubject.SUBJREGOFRNG = editedSubject.SUBJREGOFRNG;
                    existingSubject.SFSUBJCATEGORY = editedSubject.SFSUBJCATEGORY;
                    existingSubject.SUBJCURRCODE = editedSubject.SUBJCURRCODE;

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
            return _dbcontext.SubjectAndSubjectPreqs.Any(e => e.SUBJCODE == id && e.SUBJCOURSECODE==courseCode);
        }

        public IActionResult Add()
        {
            return View();
        }
     

        [HttpPost]
        public IActionResult Add(SubjectAndSubjectPreq addSubjectRequest)
        {
            // Check for duplicates excluding the current subject being edited
            var isDuplicate = _dbcontext.SubjectAndSubjectPreqs
                .Any(s => s.SUBJCODE == addSubjectRequest.SUBJCODE
                       && s.SUBJCOURSECODE == addSubjectRequest.SUBJCOURSECODE
                       && s.SUBJPRECODE == addSubjectRequest.SUBJPRECODE
                       && (s.SUBJCODE != addSubjectRequest.SUBJCODE || s.SUBJCOURSECODE != addSubjectRequest.SUBJCOURSECODE));

            if (string.IsNullOrEmpty(addSubjectRequest.SUBJCODE) ||

            string.IsNullOrEmpty(addSubjectRequest.SUBJDESC) ||

            string.IsNullOrEmpty(addSubjectRequest.SUBJUNITS.ToString()) ||

            string.IsNullOrEmpty(addSubjectRequest.SUBJREGOFRNG.ToString()) ||

            string.IsNullOrEmpty(addSubjectRequest.SFSUBJCATEGORY) ||        

            string.IsNullOrEmpty(addSubjectRequest.SUBJCOURSECODE) ||

            string.IsNullOrEmpty(addSubjectRequest.SUBJCURRCODE))

            {
                TempData["MissingFieldsError"] = "Please fill out all the required fields.";
                return View(addSubjectRequest);
            }

            if (isDuplicate)
            {
                TempData["DuplicateError"] = "Duplicate Error: The Subject Code already exists. Cannot Proceed";
                return Json(new { IsDuplicate = true });
            }

            else if (string.IsNullOrEmpty(addSubjectRequest.SUBJPRECODE))
            {
                var subject = new SubjectAndSubjectPreq()
                {
                    SUBJCODE = addSubjectRequest.SUBJCODE,
                    SUBJDESC = addSubjectRequest.SUBJDESC,
                    SUBJUNITS = addSubjectRequest.SUBJUNITS,
                    SUBJREGOFRNG = addSubjectRequest.SUBJREGOFRNG,
                    SFSUBJCATEGORY = addSubjectRequest.SFSUBJCATEGORY,
                    SUBJSTATUS = "AC",
                    SUBJCOURSECODE = addSubjectRequest.SUBJCOURSECODE,
                    SUBJCURRCODE = addSubjectRequest.SUBJCURRCODE,
                    SUBJCATEGORY = addSubjectRequest.SUBJCATEGORY,
                    SUBJPRECODE = string.IsNullOrEmpty(addSubjectRequest.SUBJPRECODE) ? null : addSubjectRequest.SUBJPRECODE
                };
                _dbcontext.SubjectAndSubjectPreqs.Add(subject);
            }

            else if (_dbcontext.SubjectAndSubjectPreqs.Any(s => s.SUBJCODE == addSubjectRequest.SUBJPRECODE))
            {

                var subject = new SubjectAndSubjectPreq()
                {
                    SUBJCODE = addSubjectRequest.SUBJCODE,
                    SUBJDESC = addSubjectRequest.SUBJDESC,
                    SUBJUNITS = addSubjectRequest.SUBJUNITS,
                    SUBJREGOFRNG = addSubjectRequest.SUBJREGOFRNG,
                    SFSUBJCATEGORY = addSubjectRequest.SFSUBJCATEGORY,
                    SUBJSTATUS = "AC",
                    SUBJCOURSECODE = addSubjectRequest.SUBJCOURSECODE,
                    SUBJCURRCODE = addSubjectRequest.SUBJCURRCODE,
                    SUBJCATEGORY = addSubjectRequest.SUBJCATEGORY,
                    SUBJPRECODE = addSubjectRequest.SUBJPRECODE
                };
                _dbcontext.SubjectAndSubjectPreqs.Add(subject);
            }
            else
            {
                TempData["DuplicateError"] = "Subject Doesn't exist";
                return View(addSubjectRequest);
            }


            _dbcontext.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult CheckForDuplicates(string subjCode, string courseCode)
        {
            var isDuplicate = _dbcontext.SubjectAndSubjectPreqs
                .Any(s => s.SUBJCODE == subjCode && s.SUBJCOURSECODE == courseCode);

            return Json(new { IsDuplicate = isDuplicate });
        }
        
    }
}

