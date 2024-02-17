using Microsoft.EntityFrameworkCore;
using Models;

namespace StudentAttendanceSystem.Data
{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) 
        : DbContext(options)
    {
        public DbSet<ClassModel> Classes { get; set; }
        public DbSet<SectionModel> Sections { get; set; }
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<AttendanceModel> Attendances { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
            => builder.Entity<ClassModel>().HasData(ClassModel.GenerateSeedData());
        
    }
}
