namespace backend.DTOs
{
    public record CreateAccountRequest
    {
        public string Email { get; init; } = string.Empty;

        public string FirstName { get; init; } = string.Empty;

        public string LastName { get; init; } = string.Empty;

        public string Password { get; init; } = string.Empty;

        public int RoleId { get; init; }

        public int DepartmentId { get; init; }

        public bool MustChangePassword { get; init; } = true;
    }
}
