namespace backend.DTOs
{
    public record WorkScheduleResponse
    {
        public int Id { get; init; }

        public int AccountId { get; init; }

        public DateTime Date { get; init; }

        public string WorkMode { get; init; } = string.Empty;
    }
}
