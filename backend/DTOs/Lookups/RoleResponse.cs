namespace backend.DTOs
{
    public record RoleResponse
    {
        public int Id { get; init; }

        public string RoleName { get; init; } = string.Empty;
    }
}
