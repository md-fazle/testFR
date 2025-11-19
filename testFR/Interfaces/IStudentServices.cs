using testFR.Models;

namespace testFR.Interfaces
{
    public interface IStudentServices
    {

        Task<List<Student>> GetAllStudentListAsync();
        Task<bool> InsertStudentAsync(Student student);
    }
}
