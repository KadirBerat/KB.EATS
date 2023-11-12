using KB.EATS.DataAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KB.EATS.DataAnalysis
{
    public class WorkingHourCalculation
    {
        private List<ShiftDataModelSimplified> model;
        private List<CalculatedShiftDataModel> calculatedModel = new List<CalculatedShiftDataModel>();
        private TimeSpan shiftHours = new TimeSpan(9, 0, 0);

        public WorkingHourCalculation(List<ShiftDataModelSimplified> model)
        {
            this.model = model;
        }

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
