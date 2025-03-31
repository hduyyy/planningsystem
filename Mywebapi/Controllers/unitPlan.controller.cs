using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mywebapi.Services;
using Mywebapi.Dtos.Department.Request;
using Mywebapi.Models;
using Mywebapi.Dtos.UnitPlan;

namespace Mywebapi.Controllers
{
    [ApiController]
    [Route("api/unitplans")]
    public class UnitPlanController : ControllerBase
    {
        private readonly UnitPlanService _unitPlanService;

        public UnitPlanController(UnitPlanService unitPlanService)
        {
            _unitPlanService = unitPlanService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UnitPlan>>> GetAllUnitPlans([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var unitPlans = await _unitPlanService.GetAllUnitPlans(page, limit);
            return Ok(unitPlans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UnitPlan>> GetUnitPlanById(int id)
        {
            var unitPlan = await _unitPlanService.GetUnitPlanById(id);
            if (unitPlan == null) return NotFound();
            return Ok(unitPlan);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUnitPlan([FromBody] CreateUnitPlanRequest request)
        {
            var unitPlan = await _unitPlanService.CreateUnitPlan(request);
            if (unitPlan == null)
            {
                return BadRequest(new { message = "Title already exists." });
            }
            return CreatedAtAction(nameof(GetUnitPlanById), new { id = unitPlan.UnitPlanId }, unitPlan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnitPlan(int id, [FromBody] UpdateUnitPlanRequest request)
        {
            var updated = await _unitPlanService.UpdateUnitPlan(id, request);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnitPlan(int id)
        {
            var deleted = await _unitPlanService.DeleteUnitPlan(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
