namespace backend.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public Role Role { get; set; } = null!;

        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

        public bool MustChangePassword { get; set; }
    }
}
