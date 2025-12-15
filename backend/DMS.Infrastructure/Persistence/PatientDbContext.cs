using DMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DMS.Infrastructure.Persistence
{
    public class PatientDbContext : DbContext
    {
        public PatientDbContext(DbContextOptions<PatientDbContext> options) : base(options) { }

        public DbSet<Patient> Patients => Set<Patient>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>()
                .HasKey(p => p.PatientId);

            modelBuilder.Entity<Patient>()
                .Property(p => p.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Patient>()
                .Property(p => p.LastName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Patient>()
                .Property(p => p.Email)
                .HasMaxLength(150);

            // Ignore PatientDocuments navigation - PatientDocumentMap is in DocumentDbContext
            modelBuilder.Entity<Patient>()
                .Ignore(p => p.PatientDocuments);
        }
    }
}
