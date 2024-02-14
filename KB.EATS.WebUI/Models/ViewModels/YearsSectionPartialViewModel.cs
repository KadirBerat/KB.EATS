namespace KB.EATS.WebUI.Models.ViewModels
{
    public class YearsSectionPartialViewModel
    {
        public List<YearData> Years { get; set; } //yıl listesi
    }

    //yıl veri modeli
    public class YearData
    {
        public int ID { get; set; }
        public string Year { get; set; }
        public byte TotalAbsenteeism { get; set; }

        public double TotalWorkingHours { get; set; }

        public double TotalOvertime { get; set; }

        public double EmployeeEfficiency { get; set; }

        public byte Rank { get; set; }
        public YearGraphData YearOvertime { get; set; }
        public YearGraphData YearEfficiency { get; set; }
        public YearGraphData YearWorkingHours { get; set; }
        public YearGraphData YearAbsenteeism { get; set; }
        public YearGraphData YearRank { get; set; }

    }

    //yıl grafik verileri
    public class YearGraphData
    {
        public string[] Labels { get; set; }
        public double[] Values { get; set; }
        public string[] Colors { get; set; }
    }

    //yıl grafik modeli
    public class YearGraphModel
    {
        public string Username { get; set; }
        public double Value { get; set; }
        public bool Current { get; set; }
    }
}
