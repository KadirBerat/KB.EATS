namespace KB.EATS.WebUI.Models.ViewModels
{
    public class WeekSectionPartialViewModel
    {
        public List<WeekData> Weeks { get; set; } //hafta listesi
    }

    //hafta veri modeli
    public class WeekData
    {
        public int ID { get; set; }
        public string Week { get; set; }
        public byte TotalAbsenteeism { get; set; }

        public double TotalWorkingHours { get; set; }

        public double TotalOvertime { get; set; }

        public double EmployeeEfficiency { get; set; }

        public byte Rank { get; set; }
        public WeekGraphData WeekOvertime { get; set; }
        public WeekGraphData WeekEfficiency { get; set; }
        public WeekGraphData WeekWorkingHours { get; set; }
        public WeekGraphData WeekAbsenteeism { get; set; }
        public WeekGraphData WeekRank { get; set; }

    }

    //hafta grafik verileri
    public class WeekGraphData
    {
        public string[] Labels { get; set; }
        public double[] Values { get; set; }
        public string[] Colors { get; set; }
    }

    //hafta grafik modeli
    public class WeekGraphModel
    {
        public string Username { get; set; }
        public double Value { get; set; }
        public bool Current { get; set; }
    }
}
