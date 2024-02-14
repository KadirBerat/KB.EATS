namespace KB.EATS.SharedModels
{
    public class DayAnalysisModel
    {
        public int UserId { get; set; }
        public DateTime ShiftDate { get; set; }
        public DateTime EntryDateTime { get; set; }
        public DateTime ExitDateTime { get; set; }
        public TimeSpan EntryTime { get; set; }
        public TimeSpan ExitTime { get; set; }
    }
}
