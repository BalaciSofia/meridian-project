using backend.Models;

namespace backend.Services.Interfaces
{
    public interface IDocumentsService
    {
        Task<Document?> GetDocumentById(int id);

        Task<IEnumerable<Document>> GetAllDocumentsForUser(int userId);

        Task<IEnumerable<DocumentType>> GetAllDocumentTypes();

        Task<IEnumerable<Document>> GetDocumentsToBeSubmitted(int userId);

        Task AddDocument(Document document);

        Task AddDocumentType(DocumentType documentType);

        Task<bool> RequireDocumentForEmployee(int accountId, int documentTypeId);

        Task UpdateDocumentStatus(int documentId, string status);

        Task SubmitDocument(int documentId, string fileUrl);
    }
}
