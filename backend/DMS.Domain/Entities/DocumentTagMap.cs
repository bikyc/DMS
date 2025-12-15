using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Domain.Entities
{
    public class DocumentTagMap
    {
        public Guid DocumentId { get; set; }
        public Document Document { get; set; } = null!;

        public int TagId { get; set; }
        public DocumentTag Tag { get; set; } = null!;
    }

}
