using backend.Models;

namespace backend.Repositories.Interfaces
{
    public interface IDocumentsRepository
    {
        Task AddDocument(Document document);
        Task AddDocuments(IEnumerable<Document> documents);
        Task AddDocumentType(DocumentType documentType);
        Task<IEnumerable<Document>> GetAllDocumentsForUser(int userId);
        Task<IEnumerable<DocumentType>> GetAllDocumentTypes();
        Task<Document?> GetDocumentById(int documentId);
        Task<Document?> GetDocumentForUserByType(int userId, int documentTypeId);
        Task<IEnumerable<Document>> GetDocumentsToBeSubmitted(int userId);
        Task UpdateDocumentStatus(Document document);
    }
}
