namespace KB.EATS.DataAnalysis.Models
{
    public class ShiftDataModel
    {
        public DateTime FullDate { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string Employee { get; set; }
    }
}
