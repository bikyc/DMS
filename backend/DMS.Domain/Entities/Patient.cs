using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class Patient
    {
        [Key]
        public Guid PatientId { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(50)]
        public string PatientCode { get; set; } = string.Empty; // Hospital internal code

        [Required]
        [MaxLength(20)]
        public string PatientNo { get; set; } = string.Empty; // Alternative hospital number

        [MaxLength(10)]
        public string? Salutation { get; set; } // Mr, Mrs, Dr, etc.

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? MiddleName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; } = string.Empty; // Could use Enum later

        [NotMapped]
        public int Age => DateOfBirth.HasValue
                          ? (int)((DateTime.Now - DateOfBirth.Value).TotalDays / 365.25)
                          : 0;

        public DateTime? DateOfBirth { get; set; }

        [MaxLength(15)]
        public string? PhoneNumber { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

        [MaxLength(20)]
        public string? PANNumber { get; set; }

        public Guid? CountrySubDivisionId { get; set; }

        public Guid? CountryId { get; set; }

        [MaxLength(5)]
        public string? BloodGroup { get; set; } // e.g., A+, O-

        [MaxLength(20)]
        public string? MaritalStatus { get; set; } // Single, Married, etc.

        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(50)]
        public string? EMPI { get; set; } // Enterprise Master Patient Index

        [MaxLength(50)]
        public string? EthnicGroup { get; set; }

        [MaxLength(100)]
        public string? Occupation { get; set; }

        public bool IsDobVerified { get; set; } = false;

        public Guid? MembershipTypeId { get; set; }

        public bool IsOutdoorPat { get; set; } = true; // OPD patient default

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [MaxLength(50)]
        public string CreatedBy { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsActive { get; set; } = true;

        [MaxLength(20)]
        public string? PassportNumber { get; set; }

        [MaxLength(50)]
        public string? ShortName { get; set; }

        // Optional: Emergency contact
        [MaxLength(50)]
        public string? EmergencyContactName { get; set; }

        [MaxLength(15)]
        public string? EmergencyContactPhone { get; set; }

        // Optional: Photo URL or path
        [MaxLength(200)]
        public string? PhotoPath { get; set; }
        public ICollection<PatientDocumentMap>? PatientDocuments { get; set; }

    }
}
