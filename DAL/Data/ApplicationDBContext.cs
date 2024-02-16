using Microsoft.EntityFrameworkCore;
using Models;

namespace StudentAttendanceSystem.Data
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        
        public DbSet<ClassModel> Classes { get; set; }
        public DbSet<SectionModel> Sections { get; set; }
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<AttendanceModel> Attendances { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ClassModel>().HasData(ClassModel.GenerateSeedData());
        }
    }
}
