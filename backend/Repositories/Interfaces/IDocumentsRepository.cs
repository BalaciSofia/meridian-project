using backend.Models;

namespace backend.Repositories.Interfaces
{
    public interface IDocumentsRepository
    {
        Task AddDocument(Document document);
        Task<IEnumerable<Document>> GetAllDocumentsForUser(int userId);
        Task<IEnumerable<DocumentType>> GetAllDocumentTypes();
        Task<Document?> GetDocumentById(int documentId);
        Task<IEnumerable<Document>> GetDocumentsToBeSubmitted(int userId);
        Task UpdateDocumentStatus(Document document);
    }
}