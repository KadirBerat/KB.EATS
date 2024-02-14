using System;
using System.Collections.Generic;

namespace KB.EATS.WebUI.Models.Entities;

public partial class Month
{
    public int MonthId { get; set; }

    public int YearsId { get; set; }

    public byte? MonthValue { get; set; }

    public byte? TotalAbsenteeism { get; set; }

    public double? TotalWorkingHours { get; set; }

    public double? TotalOvertime { get; set; }

    public double? EmployeeEfficiency { get; set; }

    public byte? Rank { get; set; }

    public double? EmployeeEfficiencyForecastForNextMonth { get; set; }

    public byte? RankForecastForNextMonth { get; set; }

    public double? EmployeeEfficiencyForecastAccuracyForThisMonth { get; set; }

    public double? RankForecastAccuracyForThisMonth { get; set; }

    public bool? IsCalculated { get; set; }

    public virtual ICollection<Week> Weeks { get; set; } = new List<Week>();

    public virtual Year Years { get; set; } = null!;
}
