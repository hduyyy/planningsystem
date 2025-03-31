using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mywebapi.Services;
using Mywebapi.Dtos.Department.Request;
using Mywebapi.Models;
using Mywebapi.Dtos.PersonalPlan;

namespace Mywebapi.Controllers
{
    [ApiController]
    [Route("api/personal-plans")]
    public class PersonalPlanController : ControllerBase
    {
        private readonly PersonalPlanService _service;
        public PersonalPlanController(PersonalPlanService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var plans = await _service.GetAll(page, limit);
            return Ok(plans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var plan = await _service.GetById(id);
            if (plan == null) return NotFound(new { message = "Không tìm thấy kế hoạch!" });
            return Ok(plan);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePersonalPlanDto dto)
        {
            var plan = await _service.Create(dto);
            if (plan == null)
            {
                return BadRequest(new { message = "Title already exists." });
            }
            return CreatedAtAction(nameof(GetById), new { id = plan.PlanId }, plan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePersonalPlanDto dto)
        {
            var updatedPlan = await _service.Update(id, dto);
            if (updatedPlan == null) return NotFound(new { message = "Không tìm thấy kế hoạch để cập nhật!" });
            return Ok(updatedPlan);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.Delete(id);
            if (!deleted) return NotFound(new { message = "Không tìm thấy kế hoạch để xóa!" });
            return NoContent();
        }
    }

}
