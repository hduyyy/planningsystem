using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mywebapi.Models;
using Mywebapi.Dtos.Department.Request;
using Mywebapi.Dtos.PersonalPlanTask;

namespace Mywebapi.Services
{
    public class PersonalPlanTaskService
    {
        private readonly AppDbContext _context;
        public PersonalPlanTaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PersonalPlanTask>> GetAll(int page, int limit)
        {
            return await _context.PersonalPlanTasks
                .OrderByDescending(p => p.DueDate)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<PersonalPlanTask?> GetById(int id)
        {
            return await _context.PersonalPlanTasks.FindAsync(id);
        }

        public async Task<PersonalPlanTask> Create(CreatePersonalPlanTaskDto dto)
        {
            bool IsExisted = await _context.PersonalPlanTasks.AnyAsync(u => u.Title == dto.Title);
            if (IsExisted)
            {
                return null;
            }
            var task = new PersonalPlanTask
            {
                PlanId = dto.PlanId,
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                DueDate = dto.DueDate
            };
            _context.PersonalPlanTasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<PersonalPlanTask?> Update(int id, UpdatePersonalPlanTaskDto dto)
        {
            var task = await _context.PersonalPlanTasks.FindAsync(id);
            if (task == null) return null;

            task.Title = dto.Title ?? task.Title;
            task.PlanId = dto.PlanId ?? task.PlanId;
            task.Description = dto.Description ?? task.Description;
            task.Status = dto.Status ?? task.Status;
            task.DueDate = dto.DueDate ?? task.DueDate;

            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> Delete(int id)
        {
            var task = await _context.PersonalPlanTasks.FindAsync(id);
            if (task == null) return false;

            _context.PersonalPlanTasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
