namespace backend.DTOs
{
    public record EventResponse
    {
        public int Id { get; init; }

        public string Title { get; init; } = string.Empty;

        public DateTime StartingTime { get; init; }

        public DateTime EndingTime { get; init; }

        public string Description { get; init; } = string.Empty;

        public string? EventLink { get; init; }

        public string? Location { get; init; }

        public int CreatedByAccountId { get; init; }

        public string CreatedByName { get; init; } = string.Empty;
    }
}
