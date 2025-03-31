using Microsoft.EntityFrameworkCore;
using Mywebapi.Models;
using Mywebapi.Dtos.ManagerEvaluation;

namespace Mywebapi.Services
{
    public class ManagerEvaluationService
    {
        private readonly AppDbContext _context;

        public ManagerEvaluationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ManagerEvaluation>> GetAll(int page, int limit)
        {
            return await _context.ManagerEvaluations
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<ManagerEvaluation?> GetById(int id)
        {
            return await _context.ManagerEvaluations.FindAsync(id);
        }

        public async Task<ManagerEvaluation> Create(CreateManagerEvaluationRequest request)
        {
            var evaluation = new ManagerEvaluation
            {
                SelfEvalId = request.SelfEvalId,
                EvaluatorId = request.EvaluatorId,
                FinalScore = request.FinalScore,
                FeedBack = request.FeedBack,
                EvaluationDate = DateTime.UtcNow
            };
            _context.ManagerEvaluations.Add(evaluation);
            await _context.SaveChangesAsync();
            return evaluation;
        }

        public async Task<bool> Update(int id, UpdateManagerEvaluationRequest request)
        {
            var evaluation = await _context.ManagerEvaluations.FindAsync(id);
            if (evaluation == null) return false;

            evaluation.FinalScore = request.FinalScore ?? evaluation.FinalScore;
            evaluation.FeedBack = request.FeedBack ?? evaluation.FeedBack;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var evaluation = await _context.ManagerEvaluations.FindAsync(id);
            if (evaluation == null) return false;

            _context.ManagerEvaluations.Remove(evaluation);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
