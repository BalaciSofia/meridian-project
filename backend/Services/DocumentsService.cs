using backend.Models;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class DocumentsService : IDocumentsService
    {
        private readonly IDocumentsRepository _documentsRepository;

        public DocumentsService(IDocumentsRepository documentsRepository)
        {
            _documentsRepository = documentsRepository;
        }

        public async Task<Document?> GetDocumentById(int id)
        {
            return await _documentsRepository.GetDocumentById(id);
        }

        public async Task<IEnumerable<Document>> GetAllDocumentsForUser(int userId)
        {
            return await _documentsRepository.GetAllDocumentsForUser(userId);
        }

        public async Task<IEnumerable<DocumentType>> GetAllDocumentTypes()
        {
            return await _documentsRepository.GetAllDocumentTypes();
        }

        public async Task<IEnumerable<Document>> GetDocumentsToBeSubmitted(int userId)
        {
            return await _documentsRepository.GetDocumentsToBeSubmitted(userId);
        }

        public async Task AddDocument(Document document)
        {
            await _documentsRepository.AddDocument(document);
        }

        public async Task UpdateDocumentStatus(int documentId, string status)
        {
            var document = await _documentsRepository.GetDocumentById(documentId);

            if (document == null)
            {
                return;
            }

            document.Status = status;
            await _documentsRepository.UpdateDocumentStatus(document);
        }

        public async Task SubmitDocument(int documentId, string fileUrl)
        {
            var document = await _documentsRepository.GetDocumentById(documentId);

            if (document == null)
            {
                return;
            }

            document.FileUrl = fileUrl;
            document.Status = "submitted";

            await _documentsRepository.UpdateDocumentStatus(document);
        }
    }
}
