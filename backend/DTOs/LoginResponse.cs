namespace backend.DTOs
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public int AccountId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public bool MustChangePassword { get; set; }
    }
}
