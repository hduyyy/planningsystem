using System;
using System.Collections.Generic;

namespace Mywebapi.Models;

public partial class PersonalPlanTask
{
    public int TaskId { get; set; }

    public int? PlanId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Status { get; set; }

    public DateOnly? DueDate { get; set; }

    public virtual ICollection<PersonalAttachment> PersonalAttachments { get; set; } = new List<PersonalAttachment>();

    public virtual ICollection<PersonalPlanApproval> PersonalPlanApprovals { get; set; } = new List<PersonalPlanApproval>();

    public virtual PersonalPlan? Plan { get; set; }
}
