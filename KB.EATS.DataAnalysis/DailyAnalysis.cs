using KB.EATS.SharedModels;

namespace KB.EATS.DataAnalysis
{
    /// <summary>
    /// Veritabanına yeni eklenen günün analizini yapar
    /// </summary>
    public class DailyAnalysis
    {
        private DayAnalysisModel model;
        private EfficiencyCalculation ec; // Verimlilik hesaplaması için kullanılacak sınıf

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">analiz edilecek günün modeli</param>
        public DailyAnalysis(DayAnalysisModel model)
        {
            this.model = model;
            this.ec = new EfficiencyCalculation();
        }

        /// <summary>
        /// Analiz işlemini yapar
        /// </summary>
        /// <returns>analiz edilmiş gün veri modeli</returns>
        public AnalyzedDayModel Analyze()
        {
            AnalyzedDayModel response = new AnalyzedDayModel();
            response.IsCalculated = true;
            response.IsAbsenteeism = false;
            response.EntryTime = model.EntryDateTime;
            response.ExitTime = model.ExitDateTime;

            //çalışma saatleri hesaplanıyor
            TimeSpan wh = model.ExitTime - model.EntryTime;
            float whF = Convert.ToSingle(wh.TotalHours);
            response.WorkingHours = whF;

            //fazla veya eksik çalışma süresi hesaplanıyor
            float ot = 9 - whF;
            response.Overtime = ot;

            //verimlilik hesaplanıyor
            response.EmployeeEfficiency = ec.Calculate(whF);

            return response;
        }
    }
}
