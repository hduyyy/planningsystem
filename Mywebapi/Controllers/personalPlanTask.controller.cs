using Microsoft.AspNetCore.Mvc;
using Mywebapi.Services;
using Mywebapi.Dtos.PersonalPlanTask;
using System.Numerics;

namespace Mywebapi.Controllers
{
    [ApiController]
    [Route("api/personal-plan-tasks")]
    public class PersonalPlanTaskController : ControllerBase
    {
        private readonly PersonalPlanTaskService _service;
        public PersonalPlanTaskController(PersonalPlanTaskService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var tasks = await _service.GetAll(page, limit);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _service.GetById(id);
            if (task == null) return NotFound(new { message = "Không tìm thấy công việc!" });
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePersonalPlanTaskDto dto)
        {
            var task = await _service.Create(dto);
            if (task == null)
            {
                return BadRequest(new { message = "Title already exists." });
            }
            return CreatedAtAction(nameof(GetById), new { id = task.TaskId }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePersonalPlanTaskDto dto)
        {
            var updatedTask = await _service.Update(id, dto);
            if (updatedTask == null) return NotFound(new { message = "Không tìm thấy công việc để cập nhật!" });
            return Ok(updatedTask);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.Delete(id);
            if (!deleted) return NotFound(new { message = "Không tìm thấy công việc để xóa!" });
            return NoContent();
        }
    }

}
