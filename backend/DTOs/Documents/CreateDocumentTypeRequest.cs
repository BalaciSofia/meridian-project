namespace backend.DTOs
{
    public record CreateDocumentTypeRequest
    {
        public string Name { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

        public bool RequiredForAll { get; init; }

        public int? RequiredForDepartmentId { get; init; }
    }
}
