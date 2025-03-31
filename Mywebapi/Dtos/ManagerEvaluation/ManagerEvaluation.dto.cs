namespace Mywebapi.Dtos.ManagerEvaluation
{
    public class CreateManagerEvaluationRequest
    {
        public int SelfEvalId { get; set; }
        public int EvaluatorId { get; set; }
        public int FinalScore { get; set; }
        public string? FeedBack { get; set; }
    }

    public class UpdateManagerEvaluationRequest
    {
        public int? FinalScore { get; set; }
        public string? FeedBack { get; set; }
    }
}
