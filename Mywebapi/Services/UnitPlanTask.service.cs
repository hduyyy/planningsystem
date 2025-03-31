
using Microsoft.EntityFrameworkCore;
using Mywebapi.Models;
using Mywebapi.Dtos.UnitPlanTask;

namespace Mywebapi.Services
{
    public class UnitPlanTaskService
    {
        private readonly AppDbContext _context;

        public UnitPlanTaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UnitPlanTask>> GetAllUnitPlanTasks(int page, int limit)
        {
            return await _context.UnitPlanTasks
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<UnitPlanTask?> GetUnitPlanTaskById(int id)
        {
            return await _context.UnitPlanTasks.FindAsync(id);
        }

        public async Task<UnitPlanTask> CreateUnitPlanTask(CreateUnitPlanTaskRequest request)
        {
            bool checkIsexisted = await _context.UnitPlanTasks.AnyAsync(u => u.Title == request.Title);
            if (checkIsexisted)
            {
                return null;
            }
            var UnitPlanTask = new UnitPlanTask
            {
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                AssignedTo = request.AssignedTo,
                UnitPlanId = request.UnitPlanId,
                Duedate = request.DueDate
            };

            _context.UnitPlanTasks.Add(UnitPlanTask);
            await _context.SaveChangesAsync();
            return UnitPlanTask;
        }

        public async Task<bool> UpdateUnitPlanTask(int id, UpdateUnitPlanTaskRequest request)
        {
            var unitPlanTask = await _context.UnitPlanTasks.FindAsync(id);
            if (unitPlanTask == null)
                return false;

            unitPlanTask.Title = request.Title ?? unitPlanTask.Title;
            unitPlanTask.Description = request.Description ?? unitPlanTask.Description;
            unitPlanTask.AssignedTo = request.AssignedTo ?? unitPlanTask.AssignedTo;
            unitPlanTask.Status = request.Status ?? unitPlanTask.Status;
            unitPlanTask.Duedate = request.DueDate ?? unitPlanTask.Duedate;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUnitPlanTask(int id)
        {
            var unitPlanTask = await _context.UnitPlanTasks.FindAsync(id);
            if (unitPlanTask == null)
                return false;

            _context.UnitPlanTasks.Remove(unitPlanTask);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
