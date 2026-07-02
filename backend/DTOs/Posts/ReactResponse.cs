namespace backend.DTOs
{
    public record ReactResponse
    {
        public int Id { get; init; }

        public int PostId { get; init; }

        public int AccountId { get; init; }

        public string ReactType { get; init; } = string.Empty;
    }
}
