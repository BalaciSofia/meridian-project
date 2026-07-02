namespace backend.DTOs
{
    public record CreateReactRequest
    {
        public string ReactType { get; init; } = string.Empty;
    }
}
