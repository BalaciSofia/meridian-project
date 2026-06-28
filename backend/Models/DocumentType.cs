namespace backend.Models
{
    public class DocumentType
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool RequiredForAll { get; set; }

        public int? RequiredForDepartmentId { get; set; }

        public Department? RequiredForDepartment { get; set; }
    }
}
