using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class DocumentTag
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        [MaxLength(50)]
        public string TagName { get; set; } = string.Empty;

        // Navigation
        public ICollection<DocumentTagMap> DocumentTagMaps { get; set; } = new List<DocumentTagMap>();
    }

}
