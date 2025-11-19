using System.Runtime.CompilerServices;
using testFR.DAL;
using testFR.Interfaces;
using testFR.Models;
using testFR.ViewModels;

namespace testFR.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly StudentsDataAccessLayer _studentsDataAccessLayer;

        public StudentServices(StudentsDataAccessLayer studentsDataAccessLayer)
        {
            _studentsDataAccessLayer = studentsDataAccessLayer;
        }

        public async Task<List<Student>> GetAllStudentListAsync()
        {
            var data = await _studentsDataAccessLayer.GetAllStudentList();
            if (data == null || data.Count == 0)
            {
                return new List<Student>();
            }

            return data;
        }

        public Task<bool> InsertStudentAsync(Student student)
        {
            return _studentsDataAccessLayer.InsertStudentsAsync(student);
        }
    }
}
