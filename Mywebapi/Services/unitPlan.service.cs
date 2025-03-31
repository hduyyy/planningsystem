using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mywebapi.Models;
using Mywebapi.Dtos.Department.Request;
using Mywebapi.Dtos.UnitPlan;

namespace Mywebapi.Services
{
    public class UnitPlanService
    {
        private readonly AppDbContext _context;

        public UnitPlanService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UnitPlan>> GetAllUnitPlans(int page, int limit)
        {
            return await _context.UnitPlans
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<UnitPlan?> GetUnitPlanById(int id)
        {
            return await _context.UnitPlans.FindAsync(id);
        }

        public async Task<UnitPlan> CreateUnitPlan(CreateUnitPlanRequest request)
        {
            bool userIsExisted = await _context.UnitPlans.AnyAsync(u => u.Title == request.Title);
            if (userIsExisted)
            {
                return null;
            }
            var unitPlan = new UnitPlan
            {
                Title = request.Title,
                Description = request.Description,
                CreatedBy = request.CreatedBy,
                DepartmentId = request.DepartmentId,
                AssignedTo = request.AssignedTo,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Status = request.Status,
                CreateAt = DateTime.UtcNow
            };

            _context.UnitPlans.Add(unitPlan);
            await _context.SaveChangesAsync();
            return unitPlan;
        }

        public async Task<bool> UpdateUnitPlan(int id, UpdateUnitPlanRequest request)
        {
            var unitPlan = await _context.UnitPlans.FindAsync(id);
            if (unitPlan == null)
                return false;

            unitPlan.Title = request.Title ?? unitPlan.Title;
            unitPlan.Description = request.Description ?? unitPlan.Description;
            unitPlan.AssignedTo = request.AssignedTo ?? unitPlan.AssignedTo;
            unitPlan.StartDate = request.StartDate ?? unitPlan.StartDate;
            unitPlan.EndDate = request.EndDate ?? unitPlan.EndDate;
            unitPlan.Status = request.Status ?? unitPlan.Status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUnitPlan(int id)
        {
            var unitPlan = await _context.UnitPlans.FindAsync(id);
            if (unitPlan == null)
                return false;

            _context.UnitPlans.Remove(unitPlan);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
