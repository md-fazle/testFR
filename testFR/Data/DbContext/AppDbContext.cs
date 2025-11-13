using Microsoft.EntityFrameworkCore;
using testFR.Models;

namespace testFR.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentMarks> StudentMarks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Decimal precision for marks
            modelBuilder.Entity<StudentMarks>()
                .Property(sm => sm.S_Mark)
                .HasPrecision(5, 2); // max 999.99, 2 decimal places

            base.OnModelCreating(modelBuilder);
        }


    }
}