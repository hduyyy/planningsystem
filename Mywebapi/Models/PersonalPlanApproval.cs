using System;
using System.Collections.Generic;

namespace Mywebapi.Models;

public partial class PersonalPlanApproval
{
    public int ApprovalId { get; set; }

    public int? TaskId { get; set; }

    public int? ApprovalBy { get; set; }

    public DateTime? ApprovalDate { get; set; }

    public string? Status { get; set; }

    public string? Remark { get; set; }

    public virtual PersonalPlanTask? Task { get; set; }
}
