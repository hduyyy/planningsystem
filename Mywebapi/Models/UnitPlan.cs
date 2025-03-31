using System;
using System.Collections.Generic;

namespace Mywebapi.Models;

public partial class UnitPlan
{
    public int UnitPlanId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int? CreatedBy { get; set; }

    public int? DepartmentId { get; set; }

    public int? AssignedTo { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Status { get; set; }

    public DateTime? CreateAt { get; set; }

    public virtual User? AssignedToNavigation { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<UnitPlanTask> UnitPlanTasks { get; set; } = new List<UnitPlanTask>();
}
