using System.ComponentModel.DataAnnotations;

namespace Mywebapi.Dtos.UnitPlanApproval
{
    public class CreateUnitPlanApprovalRequest
    {
        [Required]
        public int UnitTaskId { get; set; }

        [Required]
        public int ApprovalBy { get; set; }

        public string? Status { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public string? Remark { get; set; }
    }

    public class UpdateUnitPlanApprovalRequest
    {
        public string? Status { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public string? Remark { get; set; }
    }
}
