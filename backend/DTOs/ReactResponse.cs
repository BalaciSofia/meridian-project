namespace backend.DTOs
{
    public class ReactResponse
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public int AccountId { get; set; }

        public string ReactType { get; set; } = string.Empty;
    }
}
