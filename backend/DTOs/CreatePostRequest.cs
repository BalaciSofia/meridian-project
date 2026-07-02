namespace backend.DTOs
{
    public class CreatePostRequest
    {
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public int CreatedByAccountId { get; set; }
    }
}
