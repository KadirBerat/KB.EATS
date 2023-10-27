using KB.EATS.DataAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KB.EATS.DataAnalysis
{
    public class SampleDataConversion
    {
        List<ShiftDataModel> shifts = new List<ShiftDataModel>();
        List<ShiftDataModelSimplified> simplifiedShifts = new List<ShiftDataModelSimplified>();

        public SampleDataConversion(string dataPath)
        {
            string[] lines = File.ReadAllLines(dataPath);
            foreach (string line in lines)
            {
                string[] lineData = line.Split('_');
                string inputDate = $"{lineData[0]} {lineData[1]}";
                string format = "dd-MM-yyyy HH-mm-ss";

                DateTime date = DateTime.ParseExact(inputDate, format, null);
                shifts.Add(new ShiftDataModel()
                {
                    FullDate = date,
                    Date = DateOnly.FromDateTime(date),
                    Time = TimeOnly.FromDateTime(date),
                    Employee = Convert.ToString(lineData[2])
                });
            }
        }

        public List<ShiftDataModel> Get()
        {
            return shifts;
        }

        public List<ShiftDataModelSimplified> GetSimplified()
        {
            List<ShiftDataModel>  orderedShifts = shifts.OrderBy(x=>x.Employee).ToList();
            for (int i = 0; i < orderedShifts.Count; i+=2)
            {
                simplifiedShifts.Add(new ShiftDataModelSimplified()
                {
                    Date = orderedShifts[i].FullDate,
                    Employee = orderedShifts[i].Employee,
                    Entry = orderedShifts[i],
                    Exit = orderedShifts[i + 1]
                });
            }
            simplifiedShifts = simplifiedShifts.OrderBy(x=>x.Date).ToList();
            return simplifiedShifts;
        }
    }
}
