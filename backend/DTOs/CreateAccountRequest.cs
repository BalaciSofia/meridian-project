namespace backend.DTOs
{
    public class CreateAccountRequest
    {
        public string Email { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public int DepartmentId { get; set; }

        public bool MustChangePassword { get; set; } = true;
    }
}
