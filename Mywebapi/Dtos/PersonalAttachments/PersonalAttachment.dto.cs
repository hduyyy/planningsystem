using System.ComponentModel.DataAnnotations;

namespace Mywebapi.Dtos.PersonalAttachments
{
    public class CreatePersonalAttachmentRequest
    {
        [Required]
        public int UnitPlanId { get; set; }

        [Required]
        public string FilePath { get; set; } = null!;

        [Required]
        public int UploadedBy { get; set; }

        public DateTime? UploadedAt { get; set; }
    }

    public class UpdatePersonalAttachmentRequest
    {
        public string? FilePath { get; set; }

        public DateTime? UploadedAt { get; set; }
    }
}
