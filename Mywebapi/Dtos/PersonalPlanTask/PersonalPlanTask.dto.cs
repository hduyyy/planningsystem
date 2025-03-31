namespace Mywebapi.Dtos.PersonalPlanTask
{
    public class CreatePersonalPlanTaskDto
    {
        public int? PlanId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Status { get; set; }
        public DateOnly? DueDate { get; set; }
    }

    public class UpdatePersonalPlanTaskDto
    {
        public int? PlanId { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public DateOnly? DueDate { get; set; }
    }
}
