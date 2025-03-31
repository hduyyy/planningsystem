using System;
using System.Collections.Generic;

namespace Mywebapi.Models;

public partial class UnitPlanApproval
{
    public int ApprovalId { get; set; }

    public int? UnitTaskId { get; set; }

    public int? ApprovalBy { get; set; }

    public string? Status { get; set; }

    public DateTime? ApprovalDate { get; set; }

    public string? Remark { get; set; }

    public virtual UnitPlanTask? UnitTask { get; set; }
}
