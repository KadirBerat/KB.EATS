namespace KB.EATS.WebUI.Models.ViewModels
{
    public class DaySectionViewModel
    {
        public List<DayData> Days { get; set; } //gün verileri
    }

    //gün veri modeli
    public class DayData
    {
        public int ID { get; set; }
        public string Day { get; set; }
        public DateTime? Entry { get; set; }
        public DateTime? Exit { get; set; }
        public byte Absenteeism { get; set; }

        public double WorkingHours { get; set; }

        public double Overtime { get; set; }

        public double Efficiency { get; set; }

        public byte Rank { get; set; }
        public DayGraphData DayOvertime { get; set; }
        public DayGraphData DayEfficiency { get; set; }
        public DayGraphData DayWorkingHours { get; set; }
        public DayGraphData DayAbsenteeism { get; set; }
        public DayGraphData DayRank { get; set; }

    }

    //gün grafik verileri
    public class DayGraphData
    {
        public string[] Labels { get; set; }
        public double[] Values { get; set; }
        public string[] Colors { get; set; }
    }

    //gün grafik modeli
    public class DayGraphModel
    {
        public string Username { get; set; }
        public double Value { get; set; }
        public bool Current { get; set; }
    }
}
