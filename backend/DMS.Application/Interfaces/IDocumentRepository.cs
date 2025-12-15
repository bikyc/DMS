using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Application.Interfaces
{
    public interface IDocumentRepository
    {
        Task<IEnumerable<Document>> GetAllAsync();
        Task<Document?> GetByIdAsync(Guid documentId);
        Task AddAsync(Document document);
        Task UpdateAsync(Document document);
        Task DeleteAsync(Document document);
        Task<bool> ExistsAsync(Guid documentId);
    }

}
