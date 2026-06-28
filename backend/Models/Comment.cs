namespace backend.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; } = null!;

        public int AccountId { get; set; }

        public Account Account { get; set; } = null!;

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
