using System;
using System.Collections.Generic;

namespace Mywebapi.Models;

public partial class UnitAttachment
{
    public int AttachmentId { get; set; }

    public int? UnitPlanTaskId { get; set; }

    public string? FilePath { get; set; }

    public int? UploadedBy { get; set; }

    public DateTime? UploadedAt { get; set; }

    public virtual UnitPlanTask? UnitPlanTask { get; set; }
}
