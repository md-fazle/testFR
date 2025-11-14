using Microsoft.EntityFrameworkCore;
using testFR.Data;
using testFR.Models;
using testFR.ViewModels;

namespace testFR.DAL
{
    public class StudentsDataAccessLayer
    {
        private readonly AppDbContext _context;

        public StudentsDataAccessLayer(AppDbContext context)
        {
            _context = context;
        }

        // INSERT SUBJECT
        public async Task<bool> InsertSubjectAsync(int subId, string subName)
        {
            try
            {
                var subject = new Subjects
                {
                    Sub_id = subId,
                    Sub_Name = subName
                };

                await _context.Subjects.AddAsync(subject);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"InsertSubject Error: {ex.Message}");
                return false;
            }
        }

        // UPDATE SUBJECT
        public async Task<bool> UpdateSubjectAsync(int subId, string subName)
        {
            try
            {
                var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.Sub_id == subId);

                if (subject == null)
                    return false;

                subject.Sub_Name = subName;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UpdateSubject Error: {ex.Message}");
                return false;
            }
        }

        // DELETE SUBJECT
        public async Task<bool> DeleteSubjectAsync(int subId)
        {
            try
            {
                var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.Sub_id == subId);

                if (subject == null)
                    return false;

                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DeleteSubject Error: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Subjects>> GetAllSubjectListAsync()
        {
            return await _context.Subjects.AsNoTracking().OrderBy(sub => sub.Sub_id).ToListAsync();  
                
        }

        // JOIN QUERY
        public async Task<List<StudentDetailsViewModel>> GetStudentDetailsAsync()
        {
            return await (
                from s in _context.Students.AsNoTracking()
                join m in _context.StudentMarks.AsNoTracking() on s.S_id equals m.S_id
                join sub in _context.Subjects.AsNoTracking() on m.Sub_id equals sub.Sub_id
                select new StudentDetailsViewModel
                {
                    S_id = s.S_id,
                    Name = s.Name,
                    Email = s.Email,
                    Phone = s.Phone,
                    Sub_Name = sub.Sub_Name,
                    S_Mark = m.S_Mark
                }
            ).ToListAsync();
        }
    }
}
