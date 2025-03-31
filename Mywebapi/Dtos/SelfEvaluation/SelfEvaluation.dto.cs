namespace Mywebapi.Dtos.SelfEvaluation
{
    public class CreateSelfEvaluationRequest
    {
        public int? Score { get; set; }
        public string? Comment { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public string? Status { get; set; }
        public int? UserId { get; set; }
        public int? CriteriaId { get; set; }
    }

    public class UpdateSelfEvaluationRequest
    {
        public int? Score { get; set; }
        public string? Comment { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public string? Status { get; set; }
    }
}
