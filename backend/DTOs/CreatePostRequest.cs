namespace backend.DTOs
{
    public record CreatePostRequest
    {
        public string Title { get; init; } = string.Empty;

        public string Content { get; init; } = string.Empty;

        public string? ImageUrl { get; init; }

        public int CreatedByAccountId { get; init; }
    }
}
