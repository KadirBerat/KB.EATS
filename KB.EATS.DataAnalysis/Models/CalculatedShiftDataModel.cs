using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KB.EATS.DataAnalysis.Models
{
    public class CalculatedShiftDataModel
    {
        public string Employee { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan HoursWorked { get; set; }
        public float HoursWorkedF { get; set; }
        public TimeSpan Overtime { get; set; }
        public float OvertimeF { get; set; }
    }
}
