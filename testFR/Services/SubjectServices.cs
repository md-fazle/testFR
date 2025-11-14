using System.Threading.Tasks;
using testFR.DAL;
using testFR.Interfaces;
using testFR.ViewModels;

namespace testFR.Services
{
    public class SubjectServices :ISubjectServices
    {
        private readonly StudentsDataAccessLayer _studentsDataAccessLayer;

        public SubjectServices(StudentsDataAccessLayer studentsDataAccessLayer)
        {
            _studentsDataAccessLayer = studentsDataAccessLayer;
        }

        public async Task<bool> InsertSubjects(int sub_id, string sub_name)
        {
            if (string.IsNullOrEmpty(sub_name))
            {
                return false;

            }
            return await _studentsDataAccessLayer.InsertSubjectAsync(sub_id, sub_name);
        }

        public async Task<bool> UpdateSubjects(int sub_id, string sub_name)
        {
            if (sub_id <= 0) 
                return false;
            if(string.IsNullOrWhiteSpace(sub_name)) 
                return false;

            return await _studentsDataAccessLayer.UpdateSubjectAsync(sub_id, sub_name);
     
        }

        public async Task<bool> DeleteSubject(int sub_id)
        {
            if(sub_id <= 0)
                return false;
            return await _studentsDataAccessLayer.DeleteSubjectAsync(sub_id);
        }


        public async Task<List<StudentDetailsViewModel>> GetStudentDetailsAsync()
        {
                 var data  = await _studentsDataAccessLayer.GetStudentDetailsAsync();   

                 if(data  == null || data.Count == 0)
                  {
                     return new List<StudentDetailsViewModel>();
                  }

                 return data;
           }   
    }
}
