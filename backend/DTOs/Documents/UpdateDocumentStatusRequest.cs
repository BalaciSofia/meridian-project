namespace backend.DTOs
{
    public record UpdateDocumentStatusRequest
    {
        public string Status { get; init; } = string.Empty;
    }
}
