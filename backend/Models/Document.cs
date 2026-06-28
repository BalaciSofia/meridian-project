namespace backend.Models
{
    public class Document
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public Account Account { get; set; } = null!;

        public int DocumentTypeId { get; set; }

        public DocumentType DocumentType { get; set; } = null!;

        public string? FileUrl { get; set; }

        public string Status { get; set; } = "missing";
    }
}
