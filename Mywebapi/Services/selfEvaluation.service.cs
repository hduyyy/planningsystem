using Microsoft.EntityFrameworkCore;
using Mywebapi.Models;
using Mywebapi.Dtos.SelfEvaluation;

namespace Mywebapi.Services
{
    public class SelfEvaluationService
    {
        private readonly AppDbContext _context;

        public SelfEvaluationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SelfEvaluation>> GetAllSelfEvaluations(int page, int limit)
        {
            return await _context.SelfEvaluations
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<SelfEvaluation?> GetSelfEvaluationById(int id)
        {
            return await _context.SelfEvaluations.FindAsync(id);
        }

        public async Task<SelfEvaluation> CreateSelfEvaluation(CreateSelfEvaluationRequest request)
        {
            var selfEvaluation = new SelfEvaluation
            {
                Score = request.Score,
                Comment = request.Comment,
                Submissondate = request.SubmissionDate ?? DateTime.UtcNow,
                Status = request.Status,
                UserId = request.UserId,
                CriteriaId = request.CriteriaId
            };

            _context.SelfEvaluations.Add(selfEvaluation);
            await _context.SaveChangesAsync();
            return selfEvaluation;
        }

        public async Task<bool> UpdateSelfEvaluation(int id, UpdateSelfEvaluationRequest request)
        {
            var selfEvaluation = await _context.SelfEvaluations.FindAsync(id);
            if (selfEvaluation == null)
                return false;

            selfEvaluation.Score = request.Score ?? selfEvaluation.Score;
            selfEvaluation.Comment = request.Comment ?? selfEvaluation.Comment;
            selfEvaluation.Submissondate = request.SubmissionDate ?? selfEvaluation.Submissondate;
            selfEvaluation.Status = request.Status ?? selfEvaluation.Status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSelfEvaluation(int id)
        {
            var selfEvaluation = await _context.SelfEvaluations.FindAsync(id);
            if (selfEvaluation == null)
                return false;

            _context.SelfEvaluations.Remove(selfEvaluation);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}