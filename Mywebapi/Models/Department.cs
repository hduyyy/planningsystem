using System;
using System.Collections.Generic;

namespace Mywebapi.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreateAt { get; set; }

    public virtual ICollection<UnitPlan> UnitPlans { get; set; } = new List<UnitPlan>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
