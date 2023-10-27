using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KB.EATS.DataAnalysis.Models
{
    public class ShiftDataModelSimplified
    {
        public DateTime Date { get; set; }
        public string Employee { get; set; }
        public ShiftDataModel Entry { get; set; }
        public ShiftDataModel Exit { get; set; }
    }
}
