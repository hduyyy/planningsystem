using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mywebapi.Services;
using Mywebapi.Dtos.Department.Request;
using Mywebapi.Models;
using Mywebapi.Dtos.UnitPlanTask;

namespace Mywebapi.Controllers
{
    [ApiController]
    [Route("api/unitplantasks")]
    public class UnitPlanTaskController : ControllerBase
    {
        private readonly UnitPlanTaskService _unitPlanTaskService;

        public UnitPlanTaskController(UnitPlanTaskService unitPlanTaskService)
        {
            _unitPlanTaskService = unitPlanTaskService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UnitPlanTask>>> GetAllUnitPlanTasks([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var unitPlanTasks = await _unitPlanTaskService.GetAllUnitPlanTasks(page, limit);
            return Ok(unitPlanTasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UnitPlanTask>> GetUnitPlanTaskById(int id)
        {
            var unitPlanTask = await _unitPlanTaskService.GetUnitPlanTaskById(id);
           
            if (unitPlanTask == null) return NotFound();
            return Ok(unitPlanTask);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUnitPlanTask([FromBody] CreateUnitPlanTaskRequest request)
        {
            var unitPlanTask = await _unitPlanTaskService.CreateUnitPlanTask(request);
            if (unitPlanTask == null)
            {
                return BadRequest(new { message = "Title already exists." });
            }
            return CreatedAtAction(nameof(GetUnitPlanTaskById), new { id = unitPlanTask.UnitTaskId }, unitPlanTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnitPlanTask(int id, [FromBody] UpdateUnitPlanTaskRequest request)
        {
            var updated = await _unitPlanTaskService.UpdateUnitPlanTask(id, request);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnitPlanTask(int id)
        {
            var deleted = await _unitPlanTaskService.DeleteUnitPlanTask(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }

}
