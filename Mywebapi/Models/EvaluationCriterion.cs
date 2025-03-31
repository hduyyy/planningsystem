using System;
using System.Collections.Generic;

namespace Mywebapi.Models;

public partial class EvaluationCriterion
{
    public int CriteriaId { get; set; }

    public string? CriteriaName { get; set; }

    public string? Description { get; set; }

    public int? MaxScore { get; set; }

    public virtual ICollection<SelfEvaluation> SelfEvaluations { get; set; } = new List<SelfEvaluation>();
}
