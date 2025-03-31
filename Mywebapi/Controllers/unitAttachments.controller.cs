using Microsoft.AspNetCore.Mvc;
using Mywebapi.Services;
using Mywebapi.Models;

namespace Mywebapi.Controllers
{
    [ApiController]
    [Route("api/unit-attachments")]
    public class UnitAttachmentController : ControllerBase
    {
        private readonly UnitAttachmentService _unitAttachmentService;

        public UnitAttachmentController(UnitAttachmentService unitAttachmentService)
        {
            _unitAttachmentService = unitAttachmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUnitAttachments([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var attachments = await _unitAttachmentService.GetAllUnitAttachments(page, limit);
            return Ok(attachments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUnitAttachmentById(int id)
        {
            var attachment = await _unitAttachmentService.GetUnitAttachmentById(id);
            if (attachment == null)
                return NotFound();
            return Ok(attachment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUnitAttachment([FromForm] IFormFile file, [FromForm] int unitPlanTaskId, [FromForm] int uploadedBy)
        {
            try
            {
                var attachment = await _unitAttachmentService.UploadAndCreateUnitAttachment(file, unitPlanTaskId, uploadedBy);
                return CreatedAtAction(nameof(GetUnitAttachmentById), new { id = attachment.AttachmentId }, attachment);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnitAttachment(int id, [FromForm] IFormFile? file, [FromForm] int unitPlanTaskId, [FromForm] int uploadedBy)
        {
            var updatedAttachment = await _unitAttachmentService.UpdateUnitAttachment(id, file, unitPlanTaskId, uploadedBy);
            if (updatedAttachment == null)
                return NotFound();
            return Ok(updatedAttachment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnitAttachment(int id)
        {
            var deleted = await _unitAttachmentService.DeleteUnitAttachment(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
    }
