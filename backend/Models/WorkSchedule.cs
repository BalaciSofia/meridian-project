namespace backend.Models
{
    public class WorkSchedule
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public Account Account { get; set; } = null!;

        public DateTime Date { get; set; }

        public string WorkMode { get; set; } = string.Empty;
    }
}
