namespace Mywebapi.Dtos.EvaluationCriterion
{
    public class CreateEvaluationCriteriaRequest
    {
        public string CriteriaName { get; set; } = null!;
        public string? Description { get; set; }
        public int MaxScore { get; set; }
    }

    public class UpdateEvaluationCriteriaRequest
    {
        public string? CriteriaName { get; set; }
        public string? Description { get; set; }
        public int? MaxScore { get; set; }
    }
}
