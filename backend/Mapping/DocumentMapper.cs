using backend.DTOs;
using backend.Models;
using Riok.Mapperly.Abstractions;

namespace backend.Mapping
{
    [Mapper]
    public partial class DocumentMapper
    {
        [MapProperty("DocumentType.Name", nameof(DocumentResponse.DocumentTypeName))]
        [MapProperty("DocumentType.Description", nameof(DocumentResponse.DocumentTypeDescription))]
        [MapperIgnoreSource(nameof(Document.Account))]
        public partial DocumentResponse ToResponse(Document document);

        public partial IEnumerable<DocumentResponse> ToResponses(IEnumerable<Document> documents);

        [MapperIgnoreSource(nameof(DocumentType.RequiredForDepartment))]
        public partial DocumentTypeResponse ToResponse(DocumentType documentType);

        public partial IEnumerable<DocumentTypeResponse> ToDocumentTypeResponses(IEnumerable<DocumentType> documentTypes);
    }
}
