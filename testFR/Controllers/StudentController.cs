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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SubjectList()
        {
            var result = await subjectServices.GetAllSubjectListAsync();

            return View(result);
        }

        [HttpGet]
        public IActionResult CreateSubject()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject(Subjects subjects)
        {
            if (ModelState.IsValid)
            {
                return View(subjects);
            }

            var result = await subjectServices.InsertSubjects(subjects.Sub_id, subjects.Sub_Name);
            if (result)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Faild to add subject");
            return View(subjects);

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

                return RedirectToAction("Index");
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
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Faild to Edit subject");
            return View();

        }
    }
}
