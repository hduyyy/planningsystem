using System.ComponentModel.DataAnnotations;

namespace Mywebapi.Dtos.UnitAttachments
{
        public class CreateUnitAttachmentRequest
        {
            public int? UnitPlanId { get; set; }

            [Required]
            public string FilePath { get; set; } = null!;

            [Required]
            public int UploadedBy { get; set; }

            public DateTime? UploadedAt { get; set; }
        }

        public class UpdateUnitAttachmentRequest
        {
            public string? FilePath { get; set; }

            public DateTime? UploadedAt { get; set; }
        }   
}
