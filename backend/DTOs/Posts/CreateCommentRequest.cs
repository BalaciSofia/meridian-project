using backend.Models;

namespace backend.DTOs
{
    public record CreateCommentRequest
    {
        public string Content { get; init; } = string.Empty;
    }
}
