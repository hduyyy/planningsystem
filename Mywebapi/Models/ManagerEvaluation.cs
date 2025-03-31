using System;
using System.Collections.Generic;

namespace Mywebapi.Models;

public partial class ManagerEvaluation
{
    public int ManagerEvalId { get; set; }

    public int? SelfEvalId { get; set; }

    public int? EvaluatorId { get; set; }

    public int? FinalScore { get; set; }

    public string? FeedBack { get; set; }

    public DateTime? EvaluationDate { get; set; }

    public virtual SelfEvaluation? SelfEval { get; set; }
}
