using System;
using System.Collections.Generic;

namespace KB.EATS.WebUI.Models.Entities;

public partial class UserShift
{
    public int UserShiftId { get; set; }

    public int UserId { get; set; }

    public DateTime? Date { get; set; }

    public virtual ICollection<ShiftData> ShiftData { get; set; } = new List<ShiftData>();

    public virtual User User { get; set; } = null!;
}
