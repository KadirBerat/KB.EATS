namespace KB.EATS.DataAnalysis.Test.Models.Entities;

public partial class Week
{
    public int WeekId { get; set; }

    public int MonthId { get; set; }

    public byte? WeekValue { get; set; }

    public byte? TotalAbsenteeism { get; set; }

    public double? TotalWorkingHours { get; set; }

    public double? TotalOvertime { get; set; }

    public double? EmployeeEfficiency { get; set; }

    public byte? Rank { get; set; }

    public bool? IsCalculated { get; set; }

    public virtual ICollection<Day> Days { get; set; } = new List<Day>();

    public virtual Month Month { get; set; } = null!;
}
