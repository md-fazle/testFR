using testFR.Models;
using testFR.ViewModels;

namespace testFR.Interfaces
{
    public interface ISubjectServices
    {
        Task<bool> InsertSubjects(int sub_id, string sub_name);
        Task<bool> UpdateSubjects(int sub_id, string sub_name);
        Task<bool> DeleteSubject(int sub_id);

        Task<List<Subjects>> GetAllSubjectListAsync();
        Task<List<StudentDetailsViewModel>> GetStudentDetailsAsync();

        

    }
}
