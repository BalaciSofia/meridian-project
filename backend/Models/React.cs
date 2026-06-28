namespace backend.Models
{
    public class React
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; } = null!;

        public int AccountId { get; set; }

        public Account Account { get; set; } = null!;

        public string ReactType { get; set; } = string.Empty;
    }
}
