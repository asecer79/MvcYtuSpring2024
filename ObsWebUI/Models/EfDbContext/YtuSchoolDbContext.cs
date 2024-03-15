using Microsoft.EntityFrameworkCore;
using ObsWebUI.Models.Entities;

namespace ObsWebUI.Models.EfDbContext
{
    public class YtuSchoolDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("server=.;database=YtuSchoolDb;trusted_connection=true;MultipleActiveResultSets=true");

            optionsBuilder.UseSqlServer("server=.;database=YtuSchoolDb;trusted_connection=true;MultipleActiveResultSets=true;TrustServerCertificate=True;");
        }

        public DbSet<Course>? Courses { get; set; }
        public DbSet<Department>? Departments { get; set; }
        public DbSet<Exam>?Exams { get; set; }
        public DbSet<ExamResult>? ExamResults { get; set; }
        public DbSet<Faculty>? Faculties  { get; set; }
        public DbSet<Instructor>? Instructors { get; set; }
        public DbSet<InstructorCourse>? InstructorCourses { get; set; }
        public DbSet<Student>? Students { get; set; }
        public DbSet<StudentCourse>? StudentCourses { get; set; }
    }
}
