using Microsoft.EntityFrameworkCore;
using System.Xml;
using testFR.Data;
using testFR.Migrations;
using testFR.Models;
using testFR.ViewModels;

namespace testFR.DAL
{
    public class StudentsDataAccessLayer
    {

        private readonly AppDbContext _appDbContext;

        public StudentsDataAccessLayer(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;   
        }

        public async Task<bool>InsertSubjects(int sub_id, string sub_name)
        {
            try
            {
                var subject = new Subjects
                {
                    Sub_id = sub_id,
                    Sub_Name = sub_name
                };
                _appDbContext.Add(subject);
                await _appDbContext.SaveChangesAsync();
                return true;

            }
            catch { 
              
                return false;
               
            }
            
        }

        public async Task<bool> UpdateSubjects(int sub_id, string sub_name)
        {
            try
            { 
                var UpSubject = await _appDbContext.Subjects.FirstOrDefaultAsync(s=>s.Sub_id == sub_id);
                if(UpSubject == null)
                {
                    return false;
                }
                else
                {
                    UpSubject.Sub_Name = sub_name;
                    await _appDbContext.SaveChangesAsync();
                    return true;

                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteSubject(int sub_id)
        {

            try
            {
                var DelStudent = await _appDbContext.Subjects.FirstOrDefaultAsync(s=>s.Sub_id == sub_id);
                if(DelStudent == null)
                {
                    return false;
                }
                else
                {
                    _appDbContext.Subjects.Remove(DelStudent);
                    await _appDbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch
            {
                return false;
            }

        }
        public async Task<List<StudentDetailsViewModel>> GetStudentDetailsAsync()
        {
            var studentDetails = await (from s in _appDbContext.Students join m in _appDbContext.StudentMarks
                                        on s.S_id equals m.S_id join sub in _appDbContext.Subjects on m.Sub_id equals sub.Sub_id
                                        select new StudentDetailsViewModel
                                        {
                                            S_id = s.S_id,
                                            Name = s.Name,
                                            Email = s.Email,
                                            Phone = s.Phone,
                                            Sub_Name = sub.Sub_Name,
                                            S_Mark = m.S_Mark

                                        }).ToListAsync();

            return studentDetails;
        }


    }
}
