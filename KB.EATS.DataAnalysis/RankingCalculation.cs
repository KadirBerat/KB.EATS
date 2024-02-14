using KB.EATS.SharedModels;

namespace KB.EATS.DataAnalysis
{
    /// <summary>
    /// Sıralama hesaplama
    /// </summary>
    public class RankingCalculation
    {
        /// <summary>
        /// Hesaplama işlemi
        /// </summary>
        /// <param name="model">sıralaması hesaplanacak veriler</param>
        /// <returns>sıralaması hesaplanmış veriler</returns>
        public List<RankingCalculationModel> Calculate(List<RankingCalculationModel> model)
        {
            var result = model.OrderByDescending(x => x.WorkingHours).ToList();
            for (int i = 0; i < result.Count; i++)
            {
                result[i].Rank = (byte)(i + 1);
            }
            return result;
        }
    }
}
