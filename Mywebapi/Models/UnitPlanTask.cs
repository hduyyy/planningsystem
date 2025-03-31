using System;
using System.Collections.Generic;

namespace Mywebapi.Models;

public partial class UnitPlanTask
{
    public int UnitTaskId { get; set; }

    public int? UnitPlanId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int? AssignedTo { get; set; }

    public string? Status { get; set; }

    public DateOnly? Duedate { get; set; }

    public virtual User? AssignedToNavigation { get; set; }

    public virtual ICollection<UnitAttachment> UnitAttachments { get; set; } = new List<UnitAttachment>();

    public virtual UnitPlan? UnitPlan { get; set; }

    public virtual ICollection<UnitPlanApproval> UnitPlanApprovals { get; set; } = new List<UnitPlanApproval>();
}
