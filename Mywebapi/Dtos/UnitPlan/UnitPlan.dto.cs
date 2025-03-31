using System.ComponentModel.DataAnnotations;

namespace Mywebapi.Dtos.UnitPlan
{
    public class CreateUnitPlanRequest
    {
        [Required]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public int? DepartmentId { get; set; }

        public int? AssignedTo { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Status { get; set; }
    }

    public class UpdateUnitPlanRequest
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public int? AssignedTo { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Status { get; set; }
    }
}
