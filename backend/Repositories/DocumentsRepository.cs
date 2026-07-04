using backend.Data;
using backend.Models;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace backend.Repositories
{
    public class DocumentsRepository : IDocumentsRepository
    {
        private readonly AppDbContext _context;

        public DocumentsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Document>> GetAllDocumentsForUser(int userId)
        {
            return await _context.Documents.Include(d => d.Account).Include(d => d.DocumentType).Where(d => d.AccountId == userId).ToListAsync();
        }

        public async Task<IEnumerable<DocumentType>> GetAllDocumentTypes()
        {
            return await _context.DocumentTypes.ToListAsync();
        }

        public async Task<IEnumerable<Document>> GetDocumentsToBeSubmitted(int userId)
        {
            return await _context.Documents.Include(d => d.Account).Include(d => d.DocumentType).Where(d => d.AccountId == userId && (d.Status == "missing" || d.Status == "rejected")).ToListAsync();
        }

        public async Task AddDocument(Document document)
        {
            await _context.AddAsync(document);
            await _context.SaveChangesAsync();
        }

        public async Task AddDocumentType(DocumentType documentType)
        {
            await _context.DocumentTypes.AddAsync(documentType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDocumentStatus(Document document)
        {
            _context.Documents.Update(document);
            await _context.SaveChangesAsync();
        }

        public async Task<Document?> GetDocumentById(int documentId)
        {
            return await _context.Documents
                .Include(d => d.Account)
                .Include(d => d.DocumentType)
                .FirstOrDefaultAsync(d => d.Id == documentId);
        }

        public async Task<Document?> GetDocumentForUserByType(int userId, int documentTypeId)
        {
            return await _context.Documents
                .FirstOrDefaultAsync(d => d.AccountId == userId && d.DocumentTypeId == documentTypeId);
        }
    }
}
