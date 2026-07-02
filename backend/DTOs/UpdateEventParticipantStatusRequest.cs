namespace backend.DTOs
{
    public record UpdateEventParticipantStatusRequest
    {
        public int Id { get; init; }

        public int EventId { get; init; }

        public int AccountId { get; init; }

        public string Status { get; init; } = string.Empty;
    }
}
