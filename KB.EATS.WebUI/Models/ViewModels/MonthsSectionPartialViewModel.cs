namespace KB.EATS.WebUI.Models.ViewModels
{
    public class MonthsSectionPartialViewModel
    {
        public List<MonthData> Months { get; set; } //ay listesi
    }

    //ay veri modeli
    public class MonthData
    {
        public int ID { get; set; }
        public string Month { get; set; }
        public byte TotalAbsenteeism { get; set; }

        public double TotalWorkingHours { get; set; }

        public double TotalOvertime { get; set; }

        public double EmployeeEfficiency { get; set; }

        public byte Rank { get; set; }
        public MonthGraphData MonthOvertime { get; set; }
        public MonthGraphData MonthEfficiency { get; set; }
        public MonthGraphData MonthWorkingHours { get; set; }
        public MonthGraphData MonthAbsenteeism { get; set; }
        public MonthGraphData MonthRank { get; set; }

    }

    //ay grafik verileri
    public class MonthGraphData
    {
        public string[] Labels { get; set; }
        public double[] Values { get; set; }
        public string[] Colors { get; set; }
    }

    //ay grafik modeli
    public class MonthGraphModel
    {
        public string Username { get; set; }
        public double Value { get; set; }
        public bool Current { get; set; }
    }
}
