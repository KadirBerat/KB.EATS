namespace KB.EATS.WebUI.Models.ViewModels
{
    public class GeneralStatisticsPartialViewModel
    {
        public string? Val { get; set; }
        public string? Title { get; set; }
        public ulong ID { get; set; }
        public byte? Absenteeism { get; set; }
        public double? WorkingHours { get; set; }
        public double? Overtime { get; set; }
        public double? Efficiency { get; set; }
        public byte? Rank { get; set; }
        public string[] Labels { get; set; } //grafik etiketleri
        public double[] Values { get; set; } //grafik değerleri
    }
}
