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
        public DbSet<User> Users { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Skills> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Resumes)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);


            modelBuilder.Entity<Resume>()
                .HasMany(r => r.Experiences)
                .WithOne(e => e.Resume)
                .HasForeignKey(e => e.ResumeId);

            modelBuilder.Entity<Resume>()
                .HasMany(r => r.Skills)
                .WithOne(s => s.Resume)
                .HasForeignKey(s => s.ResumeId);

        }
    }
}
