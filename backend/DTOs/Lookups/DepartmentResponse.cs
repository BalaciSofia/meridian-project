namespace backend.DTOs
{
    public record DepartmentResponse
    {
        public int Id { get; init; }

        public string DepartmentName { get; init; } = string.Empty;

        public string SlackLink { get; init; } = string.Empty;
    }
}
