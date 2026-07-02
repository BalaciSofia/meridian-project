namespace backend.DTOs
{
    public class EmployeeResponse
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public bool MustChangePassword { get; set; }
    }
}
