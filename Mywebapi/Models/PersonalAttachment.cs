using System;
using System.Collections.Generic;

namespace Mywebapi.Models;

public partial class PersonalAttachment
{
    public int AttachmentId { get; set; }

    public int? TaskId { get; set; }

    public string? FilePath { get; set; }

    public int? UpLoadedBy { get; set; }

    public DateTime? UpLoadedAt { get; set; }

    public virtual PersonalPlanTask? Task { get; set; }
}
