using Microsoft.EntityFrameworkCore;
using Mywebapi.Models;
using Mywebapi.Dtos.PersonalPlan;
using Azure.Core;

namespace Mywebapi.Services
{
    public class PersonalPlanService
    {
        private readonly AppDbContext _context;
        public PersonalPlanService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PersonalPlan>> GetAll(int page, int limit)
        {
            return await _context.PersonalPlans
                .OrderByDescending(p => p.CreateAt)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<PersonalPlan?> GetById(int id)
        {
            return await _context.PersonalPlans.FindAsync(id);
        }

        public async Task<PersonalPlan> Create(CreatePersonalPlanDto dto)
        {
            bool IsExisted = await _context.PersonalPlans.AnyAsync(u => u.Title == dto.Title);
            if (IsExisted)
            {
                return null;
            }
            var plan = new PersonalPlan
            {
                UserId = dto.UserId,
                Title = dto.Title,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = dto.Status,
                CreateAt = DateTime.UtcNow
            };
            _context.PersonalPlans.Add(plan);
            await _context.SaveChangesAsync();
            return plan;
        }

        public async Task<PersonalPlan?> Update(int id, UpdatePersonalPlanDto dto)
        {
            var plan = await _context.PersonalPlans.FindAsync(id);
            if (plan == null) return null;

            plan.Title = dto.Title ?? plan.Title;
            plan.UserId = dto.UserId ?? plan.UserId;
            plan.Description = dto.Description ?? plan.Description;
            plan.StartDate = dto.StartDate ?? plan.StartDate;
            plan.EndDate = dto.EndDate ?? plan.EndDate;
            plan.Status = dto.Status ?? plan.Status;

            await _context.SaveChangesAsync();
            return plan;
        }

        public async Task<bool> Delete(int id)
        {
            var plan = await _context.PersonalPlans.FindAsync(id);
            if (plan == null) return false;

            _context.PersonalPlans.Remove(plan);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
