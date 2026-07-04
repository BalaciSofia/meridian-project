namespace backend.DTOs
{
    public record LoginResponse
    {
        public string Token { get; init; } = string.Empty;

        public int AccountId { get; init; }

        public string Email { get; init; } = string.Empty;

        public string FirstName { get; init; } = string.Empty;

        public string LastName { get; init; } = string.Empty;

        public string Role { get; init; } = string.Empty;

        public string Department { get; init; } = string.Empty;

        public string DepartmentSlackLink { get; init; } = string.Empty;

        public bool MustChangePassword { get; init; }
    }
}
