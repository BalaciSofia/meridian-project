namespace backend.DTOs
{
    public record CreateReactRequest
    {
        public int AccountId { get; init; }

        public string ReactType { get; init; } = string.Empty;
    }
}
