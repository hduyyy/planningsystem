using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mywebapi.Services;
using Mywebapi.Dtos.Department.Request;
using Mywebapi.Models;
using Mywebapi.Dtos.ManagerEvaluation;

namespace Mywebapi.Controllers
{
    [ApiController]
    [Route("api/manager-evaluations")]
    public class ManagerEvaluationController : ControllerBase
    {
        private readonly ManagerEvaluationService _service;

        public ManagerEvaluationController(ManagerEvaluationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ManagerEvaluation>>> GetAll([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var evaluations = await _service.GetAll(page, limit);
            return Ok(evaluations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ManagerEvaluation>> GetById(int id)
        {
            var evaluation = await _service.GetById(id);
            if (evaluation == null) return NotFound();
            return Ok(evaluation);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateManagerEvaluationRequest request)
        {
            var evaluation = await _service.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = evaluation.ManagerEvalId }, evaluation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateManagerEvaluationRequest request)
        {
            var updated = await _service.Update(id, request);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.Delete(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
