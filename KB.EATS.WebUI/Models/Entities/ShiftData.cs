using System;
using System.Collections.Generic;

namespace KB.EATS.WebUI.Models.Entities;

public partial class ShiftData
{
    public int ShiftDataId { get; set; }

    public int UserShiftId { get; set; }

    public DateTime? FullDate { get; set; }

    public DateOnly? Date { get; set; }

    public TimeOnly? Time { get; set; }

    public virtual UserShift UserShift { get; set; } = null!;
}
