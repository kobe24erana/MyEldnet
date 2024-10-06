using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApplication.Data;
using MyMvcApplication.Models;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace MyMvcApplication.Controllers
{
    public class EnrollmentHeaderFileController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public EnrollmentHeaderFileController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var enrollmentHeader = _dbContext.EnrollmentHeaderFiles.ToList();
            return View(enrollmentHeader);
        }

        public IActionResult Add()  
        {
            return View();
        }

        //STUDENT INFO 
        [HttpGet]
        public IActionResult GetStudentInfo(string studentId)
        {
            // Retrieve student information based on the student ID
            var student = _dbContext.Students.FirstOrDefault(s => s.STUDID == studentId);

            if (student != null)
            {
                string studID = $"{student.STUDID}";
                string studFullName = $"{student.STUDLNAME}, {student.STUDFNAME}, {student.STUDMNAME}";
                string studCourse = $"{student.STUDCOURSE}";
                string studYear = $"{student.STUDYEAR}";
                // Return student information in JSON format



                return Json(new
                {
                    id = studID,
                    name = studFullName,
                    course = studCourse,
                    year = studYear

                });
            }

            // If the student is not found, return an error message
            return Json(new { error = "Student not found" });
        }

        private int totalUnits = 0;
        //ADD SUBJECT 
        [HttpGet]
        public IActionResult GetSubjects(string subjectSchedId)
        {
            // Retrieve subjects information based on the Subjects ID
            var subjectSched = _dbContext.SubjectScheds.FirstOrDefault(s => s.EDPCODE == subjectSchedId);

            if (subjectSched != null)
            {
                // Retrieve the SubjectAndSubjectPreqs related to the subjectSched
                var subject = _dbContext.SubjectAndSubjectPreqs.FirstOrDefault(s => s.SUBJCODE == subjectSched.SUBJCODE);

                if (subject != null)
                {
                    string subEdpCode = $"{subjectSched.EDPCODE}";
                    string subJcode = $"{subjectSched.SUBJCODE}";
                    string startTime = $"{subjectSched.STARTTIME}";
                    string endTime = $"{subjectSched.ENDTIME}";
                    string days = $"{subjectSched.DAYS}";
                    string room = $"{subjectSched.ROOM}";
                    string units = $"{subject.SUBJUNITS}";



                    // If units can be parsed to int, add it to totalUnits
                    if (int.TryParse(units, out int unitsValue))
                    {
                        if (totalUnits + unitsValue <= 24)
                        {
                            totalUnits += unitsValue;
                        }
                    }

                    // Return student information in JSON format
                    return Json(new
                    {
                        edpId = subEdpCode,
                        subJcode = subJcode,
                        startTime = startTime,
                        endTime = endTime,
                        days = days,
                        room = room,
                        units = units,
                        totalUnits = totalUnits,
                    });
                }
            }

            // If the student is not found, return an error message
            return Json(new { error = "Student not found" });
        }


        [HttpPost]
        [ActionName("AddEnrollment")]
        public IActionResult AddEnrollment(EnrollmentHeader model)
        {
            

            try
            {
                if (_dbContext.EnrollmentHeaderFiles.Any(eh => eh.ENRHSTUDID == model.ENRHSTUDID))
                {
                    // The record already exists, return a conflict response
                    return Conflict(new { message = "Enrollment for this student already exists." });
                }

                // Calculate total units based on the subjects enrolled


                // Add a new enrollment header record
                var enrollmentHeader = new EnrollmentHeader
                {
                    ENRHSTUDID = model.ENRHSTUDID,
                    ENRHSTUDDATEENROLL = model.ENRHSTUDDATEENROLL,
                    ENRHSTUDSCHLYR = "2023-2024", // Set the school year as needed
                    ENRHSTUDENCODER = "YourEncoderName", // Set the encoder name as needed
                    ENRHSTUDTOTALUNITS = 0, // Set the total units based on the subjects enrolled
                    ENRHSTUDSTATUS = "Active"
                };

                _dbContext.EnrollmentHeaderFiles.Add(enrollmentHeader);
                _dbContext.SaveChanges();

                return Json(new { success = true, message = "Enrollment saved successfully" });
               
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine($"Exception caught while saving enrollment. Details: {ex}");

                // Rollback the transaction if there is an exception
                




                return Json(new { success = false, error = "Error occurred while saving enrollment" });
            }

        }

    }

}

