namespace backend.DTOs
{
    public record EventParticipantResponse
    {
        public int Id { get; init; }

        public int EventId { get; init; }

        public int AccountId { get; init; }

        public string ParticipantName { get; init; } = string.Empty;

        public string Status { get; init; } = string.Empty;
    }
}
