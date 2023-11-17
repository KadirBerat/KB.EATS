using System;
using System.Collections.Generic;

namespace KB.EATS.WebApi.Models.Entities;

public partial class Year
{
    public int YearsId { get; set; }

    public int UserStatisticId { get; set; }

    public short? YearValue { get; set; }

    public byte? TotalAbsenteeism { get; set; }

    public double? TotalWorkingHours { get; set; }

    public double? TotalOvertime { get; set; }

    public double? EmployeeEfficiency { get; set; }

    public byte? Rank { get; set; }

    public double? EmployeeEfficiencyForecastForNextYear { get; set; }

    public byte? RankForecastForNextYear { get; set; }

    public double? EmployeeEfficiencyForecastAccuracyForThisYear { get; set; }

    public double? RankForecastAccuracyForThisYear { get; set; }

    public virtual ICollection<Month> Months { get; set; } = new List<Month>();

    public virtual UserStatistic UserStatistic { get; set; } = null!;
}
