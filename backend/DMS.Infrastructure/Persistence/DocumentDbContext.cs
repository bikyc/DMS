using DMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DMS.Infrastructure.Persistence
{
    public class DocumentDbContext : DbContext
    {
        public DocumentDbContext(DbContextOptions<DocumentDbContext> options) : base(options) { }

        public DbSet<Document> Documents => Set<Document>();
        public DbSet<DocumentCategory> DocumentCategories => Set<DocumentCategory>();
        public DbSet<DocumentTag> DocumentTags => Set<DocumentTag>();
        public DbSet<DocumentTagMap> DocumentTagMaps => Set<DocumentTagMap>();
        public DbSet<PatientDocumentMap> PatientDocumentMaps => Set<PatientDocumentMap>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -----------------------
            // DocumentTagMap - Many-to-Many junction with composite key
            // -----------------------
            modelBuilder.Entity<DocumentTagMap>(entity =>
            {
                entity.HasKey(dt => new { dt.DocumentId, dt.TagId });

                entity.HasOne(dt => dt.Document)
                      .WithMany(d => d.DocumentTags)
                      .HasForeignKey(dt => dt.DocumentId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(dt => dt.Tag)
                      .WithMany(t => t.DocumentTagMaps)
                      .HasForeignKey(dt => dt.TagId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // -----------------------
            // PatientDocumentMap
            // Note: Patient is in PatientDbContext, so we configure the foreign key
            // column but don't enforce navigation relationship across contexts
            // -----------------------
            modelBuilder.Entity<PatientDocumentMap>(entity =>
            {
                entity.HasKey(pdm => pdm.MapId);

                // Document relationship (same context)
                entity.HasOne(pdm => pdm.Document)
                      .WithMany(d => d.PatientMappings)
                      .HasForeignKey(pdm => pdm.DocumentId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Category relationship (same context)
                entity.HasOne(pdm => pdm.Category)
                      .WithMany(c => c.PatientDocumentMaps)
                      .HasForeignKey(pdm => pdm.CategoryId)
                      .OnDelete(DeleteBehavior.SetNull);

                // Patient relationship - configured as foreign key column only
                // Patient entity is in PatientDbContext, so we can't enforce navigation here
                entity.Property(pdm => pdm.PatientId)
                      .IsRequired();
                
                // Ignore navigation property to Patient (it's in different context)
                entity.Ignore(pdm => pdm.Patient);
            });

            // -----------------------
            // AuditLog
            // Note: UserId references User entity in UserDbContext, so no navigation configured
            // -----------------------
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.HasKey(a => a.AuditId);

                // Document relationship (same context)
                entity.HasOne(a => a.Document)
                      .WithMany()
                      .HasForeignKey(a => a.DocumentId)
                      .OnDelete(DeleteBehavior.Cascade);

                // UserId is stored but no navigation (User is in UserDbContext)
                entity.Property(a => a.UserId)
                      .IsRequired();
            });
        }
    }
}