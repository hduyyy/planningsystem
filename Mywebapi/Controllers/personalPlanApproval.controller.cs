using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mywebapi.Services;
using Mywebapi.Models;
using Mywebapi.Dtos.PersonalPlanApproval;

namespace Mywebapi.Controllers
{
    [ApiController]
    [Route("api/personalplanapprovals")]
    public class PersonalPlanApprovalController : ControllerBase
    {
        private readonly PersonalPlanApprovalService _personalPlanApprovalService;

        public PersonalPlanApprovalController(PersonalPlanApprovalService personalPlanApprovalService)
        {
            _personalPlanApprovalService = personalPlanApprovalService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonalPlanApproval>>> GetAllPersonalPlanApprovals([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var approvals = await _personalPlanApprovalService.GetAllPersonalPlanApprovals(page, limit);
            return Ok(approvals);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalPlanApproval>> GetPersonalPlanApprovalById(int id)
        {
            var approval = await _personalPlanApprovalService.GetPersonalPlanApprovalById(id);
            if (approval == null) return NotFound();
            return Ok(approval);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonalPlanApproval([FromBody] CreatePersonalPlanApprovalDto request)
        {
            var approval = await _personalPlanApprovalService.CreatePersonalPlanApproval(request);
            return CreatedAtAction(nameof(GetPersonalPlanApprovalById), new { id = approval.ApprovalId }, approval);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonalPlanApproval(int id, [FromBody] UpdatePersonalPlanApprovalDto request)
        {
            var updated = await _personalPlanApprovalService.UpdatePersonalPlanApproval(id, request);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonalPlanApproval(int id)
        {
            var deleted = await _personalPlanApprovalService.DeletePersonalPlanApproval(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
