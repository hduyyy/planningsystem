using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mywebapi.Services;
using Mywebapi.Dtos.Department.Request;
using Mywebapi.Models;
using Mywebapi.Dtos.UnitPlanApproval;

namespace Mywebapi.Controllers
{
    [ApiController]
    [Route("api/unit-plan-approvals")]
    public class UnitPlanApprovalController : ControllerBase
    {
        private readonly UnitPlanApprovalService _approvalService;

        public UnitPlanApprovalController(UnitPlanApprovalService approvalService)
        {
            _approvalService = approvalService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UnitPlanApproval>>> GetAllUnitPlanApprovals([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var approvals = await _approvalService.GetAllUnitPlanApprovals(page, pageSize);
            return Ok(approvals);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UnitPlanApproval>> GetUnitPlanApprovalById(int id)
        {
            var approval = await _approvalService.GetUnitPlanApprovalById(id);
            if (approval == null) return NotFound();
            return Ok(approval);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUnitPlanApproval([FromBody] CreateUnitPlanApprovalRequest request)
        {
            var approval = await _approvalService.CreateUnitPlanApproval(request);
            return CreatedAtAction(nameof(GetUnitPlanApprovalById), new { id = approval.ApprovalId }, approval);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnitPlanApproval(int id, [FromBody] UpdateUnitPlanApprovalRequest request)
        {
            var updated = await _approvalService.UpdateUnitPlanApproval(id, request);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnitPlanApproval(int id)
        {
            var deleted = await _approvalService.DeleteUnitPlanApproval(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
