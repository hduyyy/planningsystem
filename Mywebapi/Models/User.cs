using System;
using System.Collections.Generic;

namespace Mywebapi.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Fullname { get; set; }

    public string Username { get; set; } = null!;

    public string? PasswordHash { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? Role { get; set; }

    public int? DepartmentId { get; set; }

    public DateTime? CreateAt { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<PersonalPlan> PersonalPlans { get; set; } = new List<PersonalPlan>();

    public virtual ICollection<SelfEvaluation> SelfEvaluations { get; set; } = new List<SelfEvaluation>();

    public virtual ICollection<UnitPlan> UnitPlanAssignedToNavigations { get; set; } = new List<UnitPlan>();

    public virtual ICollection<UnitPlan> UnitPlanCreatedByNavigations { get; set; } = new List<UnitPlan>();

    public virtual ICollection<UnitPlanTask> UnitPlanTasks { get; set; } = new List<UnitPlanTask>();

    public void SetPassword(string password)
    {
        // Hash mật khẩu trước khi lưu
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
    }
}
