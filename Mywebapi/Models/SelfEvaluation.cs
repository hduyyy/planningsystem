using System;
using System.Collections.Generic;

namespace Mywebapi.Models;

public partial class SelfEvaluation
{
    public int SelfEvalId { get; set; }

    public int? Score { get; set; }

    public string? Comment { get; set; }

    public DateTime? Submissondate { get; set; }

    public string? Status { get; set; }

    public int? UserId { get; set; }

    public int? CriteriaId { get; set; }

    public virtual EvaluationCriterion? Criteria { get; set; }

    public virtual ICollection<ManagerEvaluation> ManagerEvaluations { get; set; } = new List<ManagerEvaluation>();

    public virtual User? User { get; set; }
}
