using Microsoft.AspNetCore.Mvc;
using testFR.Interfaces;
using testFR.Models;

namespace testFR.Controllers
{
    public class StudentController : Controller
    {
        private readonly ISubjectServices subjectServices;
        public StudentController(ISubjectServices subjectServices)
        {
            this.subjectServices = subjectServices;
        }

        /// <summary>
        /// for students
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        ///  Subjects 
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> SubjectList()
        {
            var subjects = await subjectServices.GetAllSubjectListAsync();
            return PartialView("SubjectList", subjects);
        }



        [HttpGet]
        public IActionResult CreateSubject()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject(Subjects subjects)
        {
            if (!ModelState.IsValid)
            {
            
                return BadRequest(ModelState);
            }

            var result = await subjectServices.InsertSubjects(subjects.Sub_id, subjects.Sub_Name);

            if (result)
            {
           
                return Ok();
            }

            return BadRequest("Failed to add subject");
        }

        [HttpPost]

        public async Task<IActionResult> EditSubjects(Subjects subjects)
        {
            if (ModelState.IsValid)
            {
                return View(subjects);
            }
            var result = await subjectServices.UpdateSubjects(subjects.Sub_id, subjects.Sub_Name);

            if (result)
            {

                return RedirectToAction("SubjectList");
            }
            ModelState.AddModelError("", "Faild to Edit subject");
     
            return View(subjects);

        }

        [HttpPost]

        public async Task<IActionResult> DeleteSubject(int sub_id)
        {
            var result = await subjectServices.DeleteSubject(sub_id);
            if (result)
            {
                return RedirectToAction("SubjectList");
            }
            ModelState.AddModelError("", "Faild to Edit subject");
            return View();

        }
    }
}
