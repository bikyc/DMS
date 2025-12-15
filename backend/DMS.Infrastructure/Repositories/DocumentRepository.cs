using DMS.Application.Interfaces;
using DMS.Domain.Entities;
using DMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DMS.Infrastructure.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DocumentDbContext _context;

        public DocumentRepository(DocumentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Document>> GetAllAsync()
        {
            return await _context.Documents.ToListAsync();
        }

        public async Task<Document?> GetByIdAsync(Guid documentId)
        {
            return await _context.Documents.FindAsync(documentId);
        }

        public async Task AddAsync(Document document)
        {
            await _context.Documents.AddAsync(document);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Document document)
        {
            _context.Documents.Update(document);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Document document)
        {
            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid documentId)
        {
            return await _context.Documents.AnyAsync(d => d.DocumentId == documentId);
        }
    }

}
