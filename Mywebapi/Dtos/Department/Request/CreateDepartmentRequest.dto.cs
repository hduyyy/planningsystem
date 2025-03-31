using System.ComponentModel.DataAnnotations;

namespace Mywebapi.Dtos.Department.Request
{
    public class CreateDepartmentRequest
    {
        [Required]
        public string DepartmentName { get; set; } = null!;

        public string? Description { get; set; }
    }
}
