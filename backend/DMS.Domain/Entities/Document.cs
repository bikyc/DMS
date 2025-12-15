using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class Document
    {
        [Key]
        public Guid DocumentId { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200)]
        public string FileName { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string OriginalFileName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string FileType { get; set; } = string.Empty; // pdf/png/dicom

        [MaxLength(100)]
        public string? MimeType { get; set; }

        public long FileSize { get; set; }

        [MaxLength(500)]
        public string StoragePath { get; set; } = string.Empty;

        [MaxLength(64)]
        public string? Hash { get; set; } // SHA256

        public int Version { get; set; } = 1;

        public Guid CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public byte Status { get; set; } = 1; // 1=Active, 2=Deleted, 3=Archived

        // Navigation properties
        public ICollection<PatientDocumentMap>? PatientMappings { get; set; }

        public ICollection<DocumentTagMap> DocumentTags { get; set; } = new List<DocumentTagMap>();
    }

}
