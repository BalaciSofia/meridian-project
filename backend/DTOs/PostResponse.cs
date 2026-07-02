namespace backend.DTOs
{
    public record PostResponse
    {
        public int Id { get; init; }

        public string Title { get; init; } = string.Empty;

        public string Content { get; init; } = string.Empty;

        public string? ImageUrl { get; init; }

        public DateTime CreatedAt { get; init; }

        public int CreatedByAccountId { get; init; }

        public string CreatedByName { get; init; } = string.Empty;
    }
}
