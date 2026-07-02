namespace backend.DTOs
{
    public class CreateReactRequest
    {
        public int AccountId { get; set; }

        public string ReactType { get; set; } = string.Empty;
    }
}
