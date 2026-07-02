namespace backend.DTOs
{
    public record CreateEventRequest
    {
        public string Title { get; init; } = string.Empty;

        public DateTime StartingTime { get; init; }

        public DateTime EndingTime { get; init; }

        public string Description { get; init; } = string.Empty;

        public string? EventLink { get; init; }

        public string? Location { get; init; }

        public IEnumerable<int> ParticipantIds { get; init; } = [];
    }
}
