using backend.Models;

namespace backend.DTOs
{
    public record CommentResponse
    {
        public int Id { get; init; }

        public int PostId { get; init; }

        public int AccountId { get; init; }

        public string Content { get; init; } = string.Empty;

        public DateTime CreatedAt { get; init; }

        public string AuthorName { get; init; } = string.Empty;
    }
}
