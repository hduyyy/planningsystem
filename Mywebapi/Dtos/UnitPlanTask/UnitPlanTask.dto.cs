using System.ComponentModel.DataAnnotations;

namespace Mywebapi.Dtos.UnitPlanTask
{
    public class CreateUnitPlanTaskRequest
    {
        [Required]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }


        public string? Status { get; set; }

        public DateOnly? DueDate { get; set; }

        public int? AssignedTo { get; set; }
            
        public int? UnitPlanId { get; set; }

    }
    public class UpdateUnitPlanTaskRequest
    {
        public string? Title { get; set; } 

        public string? Description { get; set; }


        public string? Status { get; set; }

        public DateOnly? DueDate { get; set; }

        public int? AssignedTo { get; set; }

        public int? UnitPlanId { get; set; }

    }
}
