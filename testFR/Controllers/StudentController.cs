using Microsoft.AspNetCore.Mvc;
using testFR.Interfaces;
using testFR.Models;

namespace testFR.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ISubjectServices subjectServices;
        private readonly IStudentServices studentServices;

        public StudentController(ISubjectServices subjectServices, IStudentServices studentServices)
        {
            this.subjectServices = subjectServices;
            this.studentServices = studentServices;
        }

        // ========================= SUBJECTS =========================

        [HttpGet("subjectList")]
        public async Task<IActionResult> SubjectList()
        {
            var subjects = await subjectServices.GetAllSubjectListAsync();
            return Ok(subjects);
        }

        [HttpPost("createSubject")]
        public async Task<IActionResult> CreateSubject([FromBody] Subjects subjects)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await subjectServices.InsertSubjects(subjects.Sub_id, subjects.Sub_Name);
            if (result)
                return Ok(subjects);

            return BadRequest("Failed to add subject");
        }

        [HttpPut("editSubject")]
        public async Task<IActionResult> EditSubjects([FromBody] Subjects subjects)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await subjectServices.UpdateSubjects(subjects.Sub_id, subjects.Sub_Name);
            if (result)
                return Ok(subjects);

            return BadRequest("Failed to edit subject");
        }

        [HttpDelete("deleteSubject/{sub_id}")]
        public async Task<IActionResult> DeleteSubject(int sub_id)
        {
            var result = await subjectServices.DeleteSubject(sub_id);
            if (result)
                return NoContent();

            return BadRequest("Failed to delete subject");
        }

        // ========================= STUDENTS =========================

        [HttpGet("studentList")]
        public async Task<IActionResult> StudentList()
        {
            var students = await studentServices.GetAllStudentListAsync();
            return Ok(students);
        }

        [HttpPost("createStudent")]
        public async Task<IActionResult> CreateStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await studentServices.InsertStudentAsync(student);
            if (result)
                return Ok(student);

            return BadRequest("Failed to add student");
        }

    }
}
