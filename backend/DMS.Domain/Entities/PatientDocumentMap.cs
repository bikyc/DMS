using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class PatientDocumentMap
    {
        public Guid MapId { get; set; } = Guid.NewGuid();

        public Guid DocumentId { get; set; }
        public Document Document { get; set; } = null!;

        public Guid PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        public Guid? VisitId { get; set; } // Optional if visit tracking is used

        public int? CategoryId { get; set; }
        public DocumentCategory? Category { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
