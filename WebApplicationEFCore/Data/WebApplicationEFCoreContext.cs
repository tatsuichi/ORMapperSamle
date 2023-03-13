using Microsoft.EntityFrameworkCore;
using WebApplicationEFCore.Models;

namespace WebApplicationEFCore.Data
{
    public class WebApplicationEFCoreContext : DbContext
    {
        public WebApplicationEFCoreContext (DbContextOptions<WebApplicationEFCoreContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; } = default!;
        public DbSet<Enrollment> Enrollments { get; set; } = default!;
        public DbSet<Student> Students { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
        }
    }
}
