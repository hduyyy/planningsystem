using Microsoft.AspNetCore.Mvc;
using Mywebapi.Services;
using Mywebapi.Models;
using Mywebapi.Dtos.EvaluationCriterion;

namespace Mywebapi.Controllers
{
    [ApiController]
    [Route("api/evaluation-criteria")]
    public class EvaluationCriterionController : ControllerBase
    {
        private readonly EvaluationCriterionService _service;

        public EvaluationCriterionController(EvaluationCriterionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<EvaluationCriterion>>> GetAll([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var criteria = await _service.GetAll(page, limit);
            return Ok(criteria);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EvaluationCriterion>> GetById(int id)
        {
            var criterion = await _service.GetById(id);
            if (criterion == null) return NotFound();
            return Ok(criterion);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEvaluationCriteriaRequest request)
        {
            var criterion = await _service.Create(request);
            if(criterion == null)
            {
                return BadRequest(new { message = "CriteriaName already existed" });
            }    
            return CreatedAtAction(nameof(GetById), new { id = criterion.CriteriaId }, criterion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEvaluationCriteriaRequest request)
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
