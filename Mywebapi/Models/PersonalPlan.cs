using System;
using System.Collections.Generic;

namespace Mywebapi.Models;

public partial class PersonalPlan
{
    public int PlanId { get; set; }

    public int? UserId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Status { get; set; }

    public DateTime? CreateAt { get; set; }

    public virtual ICollection<PersonalPlanTask> PersonalPlanTasks { get; set; } = new List<PersonalPlanTask>();

    public virtual User? User { get; set; }
}
