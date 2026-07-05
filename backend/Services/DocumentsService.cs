using backend.Models;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class DocumentsService : IDocumentsService
    {
        private readonly IDocumentsRepository _documentsRepository;
        private readonly IAccountRepository _accountRepository;

        public DocumentsService(
            IDocumentsRepository documentsRepository,
            IAccountRepository accountRepository)
        {
            _documentsRepository = documentsRepository;
            _accountRepository = accountRepository;
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

        public async Task AddDocumentType(DocumentType documentType)
        {
            await _documentsRepository.AddDocumentType(documentType);

            var accounts = await _accountRepository.GetAllAccounts();
            var matchingAccounts = accounts.Where(account =>
                documentType.RequiredForAll
                || account.DepartmentId == documentType.RequiredForDepartmentId);

            var documents = matchingAccounts.Select(account => new Document
            {
                AccountId = account.Id,
                DocumentTypeId = documentType.Id,
                Status = "missing"
            });

            await _documentsRepository.AddDocuments(documents);
        }

        public async Task<bool> RequireDocumentForEmployee(int accountId, int documentTypeId)
        {
            var existingDocument = await _documentsRepository.GetDocumentForUserByType(accountId, documentTypeId);

            if (existingDocument != null)
            {
                return false;
            }

            await _documentsRepository.AddDocument(new Document
            {
                AccountId = accountId,
                DocumentTypeId = documentTypeId
            });

            return true;
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
