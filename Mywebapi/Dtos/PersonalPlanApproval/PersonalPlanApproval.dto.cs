namespace Mywebapi.Dtos.PersonalPlanApproval
{
    public class CreatePersonalPlanApprovalDto
    {
        public int? TaskId { get; set; }
        public int? ApprovalBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? Status { get; set; }
        public string? Remark { get; set; }
    }
    public class UpdatePersonalPlanApprovalDto
    {
        public int? TaskId { get; set; }
        public int? ApprovalBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? Status { get; set; }
        public string? Remark { get; set; }
    }
}
