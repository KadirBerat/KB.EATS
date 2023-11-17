using System;
using System.Collections.Generic;

namespace KB.EATS.WebApi.Models.Entities;

public partial class ShiftData
{
    public int ShiftDataId { get; set; }

    public int UserShiftId { get; set; }

    public DateTime? FullDate { get; set; }

    public DateTime? Date { get; set; }

    public TimeSpan? Time { get; set; }

    public virtual UserShift UserShift { get; set; } = null!;
}
