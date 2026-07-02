namespace backend.DTOs
{
    public record SetWorkScheduleRequest
    {
        public DateTime Date { get; init; }

        public string WorkMode { get; init; } = string.Empty;
    }
}
