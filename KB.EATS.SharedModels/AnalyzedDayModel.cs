namespace KB.EATS.SharedModels
{
    public class AnalyzedDayModel
    {
        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }
        public float WorkingHours { get; set; }
        public float Overtime { get; set; }
        public float EmployeeEfficiency { get; set; }
        public byte Rank { get; set; }
        public bool IsAbsenteeism { get; set; }
        public bool IsCalculated { get; set; }
    }
}
