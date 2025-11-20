using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using testFR.Data;
using testFR.Data.DbConnection;
using testFR.Models;
using testFR.ViewModels;

namespace testFR.DAL
{
    public class StudentsDataAccessLayer
    {
        private readonly AppDbContext _context;
        private readonly DbConnection _dbConnection;

        public StudentsDataAccessLayer(AppDbContext context , DbConnection dbConnection)
        {
            _context = context;
            _dbConnection = dbConnection;
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

        // JOIN QUERY Using LinQ
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








        ///////////////////// ADO.NET ################################### Derect Query 
        
        public async Task<List<Student>> GetAllStudentList()
        {
           var students  = new List<Student>();

            const string query = @"select s.S_id,s.Name,s.Email,s.Phone from dbo.Students s";

            using var con = await _dbConnection.CreateOpenConnectionAsync();

            using var cmd = new SqlCommand(query, con);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var st = new Student
                {
                    S_id = reader.GetString(reader.GetOrdinal("S_id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    Phone = reader.GetString(reader.GetOrdinal("Phone"))
                };
                students.Add(st);
            }
            return students;
        }

        public async Task<bool> InsertAllStudentAsync(Student student)
        {
            const string query = @"
                INSERT INTO Students (S_id, Name, Email, Phone)
                VALUES (@S_id, @Name, @Email, @Phone);
            ";

            using var connection = await _dbConnection.CreateOpenConnectionAsync();
            using var cmd = new SqlCommand(query, connection);

            cmd.Parameters.Add(new SqlParameter("@S_id", SqlDbType.NVarChar, 20) { Value = student.S_id });
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = student.Name });
            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 150) { Value = student.Email });
            cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 11) { Value = student.Phone });

            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }





        //############### ADO.NET  Store Procedure Programming ########################

        public async Task<bool> InsertStudentsAsync(Student student)
        {
            try
            {
                using var con = await _dbConnection.CreateOpenConnectionAsync();
                using var cmd = new SqlCommand("sp_InsertStudent", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@S_id", SqlDbType.NVarChar, 20) { Value = student.S_id });
                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = student.Name });
                cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 150) { Value = student.Email });
                cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 15) { Value = student.Phone });

                await cmd.ExecuteNonQueryAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DAL Error: " + ex.Message);
                return false;
            }
        }




    }
}
