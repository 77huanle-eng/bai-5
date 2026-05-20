using Microsoft.EntityFrameworkCore;

namespace bai5.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Grade> Grades { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Data lớp học
            modelBuilder.Entity<Grade>().HasData(
                new Grade { GradeId = 1, GradeName = "21DTHA1" },
                new Grade { GradeId = 2, GradeName = "21DTHA2" },
                new Grade { GradeId = 3, GradeName = "21DTHA3" }
            );

            // Seed Data sinh viên
            modelBuilder.Entity<Student>().HasData(
                new Student { StudentId = 1, FirstName = "Khuyên", LastName = "Bùi", GradeId = 1 },
                new Student { StudentId = 2, FirstName = "Toàn", LastName = "Nguyễn", GradeId = 1 }
            );
        }
    }
}