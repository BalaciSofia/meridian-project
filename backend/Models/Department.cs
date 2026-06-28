namespace backend.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; } = string.Empty;

        public string SlackLink { get; set; } = string.Empty;
    }
}
