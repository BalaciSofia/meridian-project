namespace backend.Models
{
    public class Event
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime StartingTime { get; set; }

        public DateTime EndingTime { get; set; }

        public string Description { get; set; } = string.Empty;

        public string? EventLink { get; set; }

        public string? Location { get; set; }

        public int CreatedByAccountId { get; set; }

        public Account CreatedByAccount { get; set; } = null!;
    }
}
