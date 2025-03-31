using Microsoft.AspNetCore.Mvc;
using Mywebapi.Services;
using Mywebapi.Models;
using Mywebapi.Dtos.SelfEvaluation;

namespace Mywebapi.Controllers
{
    [ApiController]
    [Route("api/self-evaluations")]
    public class SelfEvaluationController : ControllerBase
    {
        private readonly SelfEvaluationService _selfEvaluationService;

        public SelfEvaluationController(SelfEvaluationService selfEvaluationService)
        {
            _selfEvaluationService = selfEvaluationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SelfEvaluation>>> GetAllSelfEvaluations([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var evaluations = await _selfEvaluationService.GetAllSelfEvaluations(page, limit);
            return Ok(evaluations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SelfEvaluation>> GetSelfEvaluationById(int id)
        {
            var evaluation = await _selfEvaluationService.GetSelfEvaluationById(id);
            if (evaluation == null) return NotFound();
            return Ok(evaluation);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSelfEvaluation([FromBody] CreateSelfEvaluationRequest request)
        {
            var evaluation = await _selfEvaluationService.CreateSelfEvaluation(request);
            return CreatedAtAction(nameof(GetSelfEvaluationById), new { id = evaluation.SelfEvalId }, evaluation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSelfEvaluation(int id, [FromBody] UpdateSelfEvaluationRequest request)
        {
            var updated = await _selfEvaluationService.UpdateSelfEvaluation(id, request);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSelfEvaluation(int id)
        {
            var deleted = await _selfEvaluationService.DeleteSelfEvaluation(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
