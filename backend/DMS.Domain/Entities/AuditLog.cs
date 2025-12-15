using DMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class AuditLog
    {
        [Key]
        public Guid AuditId { get; set; } = Guid.NewGuid();

        public Guid DocumentId { get; set; }
        public Document Document { get; set; } = null!;

        public Guid UserId { get; set; } // Could be linked to User entity

        public AuditAction Action { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [MaxLength(50)]
        public string? IP { get; set; }
    }

}
