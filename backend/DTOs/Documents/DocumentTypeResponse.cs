namespace backend.DTOs
{
    public record DocumentTypeResponse
    {
        public int Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

        public bool RequiredForAll { get; init; }

        public int? RequiredForDepartmentId { get; init; }
    }
}
