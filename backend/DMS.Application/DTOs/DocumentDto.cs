using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Application.DTOs
{
    public class DocumentDto
    {
        public Guid DocumentId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string OriginalFileName { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string? StoragePath { get; set; }
        public int Version { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
    }

    public class UploadDocumentDto
    {
        public IFormFile File { get; set; } = null!;
        public Guid? PatientId { get; set; } // optional mapping
        public int? CategoryId { get; set; } // optional category
    }

}
