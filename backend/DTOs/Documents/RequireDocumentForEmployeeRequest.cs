namespace backend.DTOs
{
    public record RequireDocumentForEmployeeRequest
    {
        public int DocumentTypeId { get; init; }
    }
}
