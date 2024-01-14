using Microsoft.EntityFrameworkCore;
using ResumeModuleApp.Models;

namespace ResumeModuleApp.DataService
{
    public class ResumeContext : DbContext
    {
        public ResumeContext(DbContextOptions<ResumeContext> options) : base(options)
        {
        }

        public DbSet<Resume> Resumes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resume>().ToTable("Resumes");
        }
    }
}
