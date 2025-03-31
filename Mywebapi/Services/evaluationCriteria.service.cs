using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mywebapi.Models;
using Mywebapi.Dtos.EvaluationCriterion;

namespace Mywebapi.Services
{
    public class EvaluationCriterionService
    {
        private readonly AppDbContext _context;

        public EvaluationCriterionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EvaluationCriterion>> GetAll(int page, int limit)
        {
            return await _context.EvaluationCriteria
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<EvaluationCriterion?> GetById(int id)
        {
            return await _context.EvaluationCriteria.FindAsync(id);
        }

        public async Task<EvaluationCriterion> Create(CreateEvaluationCriteriaRequest request)
        {
            bool checkIsExisted = await _context.EvaluationCriteria.AnyAsync(u => u.CriteriaName == request.CriteriaName);
                if(checkIsExisted)
            {
                return null;
            }    
            var criterion = new EvaluationCriterion
            {
                CriteriaName = request.CriteriaName,
                Description = request.Description,
                MaxScore = request.MaxScore
            };

            _context.EvaluationCriteria.Add(criterion);
            await _context.SaveChangesAsync();
            return criterion;
        }

        public async Task<bool> Update(int id, UpdateEvaluationCriteriaRequest request)
        {
            var criterion = await _context.EvaluationCriteria.FindAsync(id);
            if (criterion == null)
                return false;

            criterion.CriteriaName = request.CriteriaName ?? criterion.CriteriaName;
            criterion.Description = request.Description ?? criterion.Description;
            criterion.MaxScore = request.MaxScore ?? criterion.MaxScore;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var criterion = await _context.EvaluationCriteria.FindAsync(id);
            if (criterion == null)
                return false;

            _context.EvaluationCriteria.Remove(criterion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}