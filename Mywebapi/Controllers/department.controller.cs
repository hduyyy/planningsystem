
using Microsoft.AspNetCore.Mvc;
using Mywebapi.Services;
using Mywebapi.Dtos.Department.Request;
using Mywebapi.Models;

namespace Mywebapi.Controllers
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentService _departmentService;

        public DepartmentController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Department>>> GetAllDepartments([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var departments = await _departmentService.GetAllDepartments(page, limit);
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartmentById(int id)
        {
            var department = await _departmentService.GetDepartmentById(id);
            if (department == null) return NotFound();
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentRequest request)
        {
            var department = await _departmentService.CreateDepartment(request);
            if(department == null)
            {
                return BadRequest(new { message = "departmentName already exists." });
            }    
            return CreatedAtAction(nameof(GetDepartmentById), new { id = department.DepartmentId }, department);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] UpdateDepartmentRequest request)
        {
            var updated = await _departmentService.UpdateDepartment(id, request);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var deleted = await _departmentService.DeleteDepartment(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
