using System;
using System.Collections.Generic;

namespace KB.EATS.WebApi.Models.Entities;

public partial class Day
{
    public int DayId { get; set; }

    public int WeekId { get; set; }

    public byte? DayValue { get; set; }

    public DateTime? EntryTime { get; set; }

    public DateTime? ExitTime { get; set; }

    public double? WorkingHours { get; set; }

    public double? Overtime { get; set; }

    public double? EmployeeEfficiency { get; set; }

    public byte? Rank { get; set; }

    public bool? IsAbsenteeism { get; set; }

    public virtual Week Week { get; set; } = null!;
}
