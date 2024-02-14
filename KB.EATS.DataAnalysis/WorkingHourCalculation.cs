using KB.EATS.DataAnalysis.Models;

namespace KB.EATS.DataAnalysis
{
    /// <summary>
    /// Çalışma saatlerini hesaplar.
    /// </summary>
    public class WorkingHourCalculation
    {
        private List<ShiftDataModelSimplified> model;
        private List<CalculatedShiftDataModel> calculatedModel = new List<CalculatedShiftDataModel>();
        private TimeSpan shiftHours = new TimeSpan(9, 0, 0);

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="model">sadeleştirilmiş mesai veri listesi</param>
        public WorkingHourCalculation(List<ShiftDataModelSimplified> model)
        {
            this.model = model;
        }

        /// <summary>
        /// Çalışma saatlerini hesaplar.
        /// </summary>
        /// <returns>çalışma saati hesaplanmış model</returns>
        public List<CalculatedShiftDataModel> Get()
        {
            foreach (var item in model)
            {
                TimeSpan workTime = item.Exit.Time - item.Entry.Time;
                float totalHours = (float)workTime.TotalHours;

                TimeSpan overtime = workTime - shiftHours;
                float overtimeF = (float)overtime.TotalHours;

                calculatedModel.Add(new CalculatedShiftDataModel()
                {
                    Date = item.Date.Date,
                    Employee = item.Employee,
                    HoursWorked = workTime,
                    HoursWorkedF = totalHours,
                    Overtime = overtime,
                    OvertimeF = overtimeF
                });
            }
            return calculatedModel;
        }
    }
}
