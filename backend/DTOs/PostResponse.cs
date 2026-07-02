namespace backend.DTOs
{
    public class PostResponse
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CreatedByAccountId { get; set; }

        public string CreatedByName { get; set; } = string.Empty;
    }
}
