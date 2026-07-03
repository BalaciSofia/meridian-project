namespace backend.DTOs
{
    public record DocumentResponse
    {
        public int Id { get; init; }

        public int AccountId { get; init; }

        public int DocumentTypeId { get; init; }

        public string DocumentTypeName { get; init; } = string.Empty;

        public string DocumentTypeDescription { get; init; } = string.Empty;

        public string? FileUrl { get; init; }

        public string Status { get; init; } = string.Empty;
    }
}
