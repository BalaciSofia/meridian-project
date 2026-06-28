namespace backend.Models
{
    public class EventParticipant
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; } = null!;

        public int AccountId { get; set; }

        public Account Account { get; set; } = null!;

        public string Status { get; set; } = "pending";
    }
}
