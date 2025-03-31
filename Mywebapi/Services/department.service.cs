using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mywebapi.Models;
using Mywebapi.Dtos.Department.Request;

namespace Mywebapi.Services
{
    public class DepartmentService
    {
        private readonly AppDbContext _context;

        public DepartmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetAllDepartments(int page, int limit)
        {
            return await _context.Departments
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<Department?> GetDepartmentById(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task<Department> CreateDepartment(CreateDepartmentRequest request)
        {
            bool checkIsexisted=await _context.Departments.AnyAsync(u=>u.DepartmentName==request.DepartmentName);
            if (checkIsexisted) {
                return null;
            }
            var department = new Department
            {
                DepartmentName = request.DepartmentName,
                Description = request.Description,
                CreateAt = DateTime.UtcNow
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<bool> UpdateDepartment(int id, UpdateDepartmentRequest request)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
                return false;

            department.DepartmentName = request.DepartmentName ?? department.DepartmentName;
            department.Description = request.Description ?? department.Description;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
                return false;

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
