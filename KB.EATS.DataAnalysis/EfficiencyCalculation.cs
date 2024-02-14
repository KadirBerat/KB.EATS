namespace KB.EATS.DataAnalysis
{
    public class EfficiencyCalculation
    {
        /// <summary>
        /// Gün için verimlilik hesaplama
        /// </summary>
        /// <param name="workingHours">çalışma saati</param>
        /// <returns>verimlilik değeri</returns>
        public float Calculate(float workingHours)
        {
            float maxWorkingHours = 9f;
            float efficiency = 0f;

            if (workingHours <= 0) //çalışma saati 0'dan küçükse verimlilik 0
            {
                efficiency = 0f;
            }
            else if (workingHours >= maxWorkingHours) //çalışma saati 9'dan büyük veya eşitse verimlilik 100
            {
                efficiency = 100f;

                if (workingHours > maxWorkingHours) //çalışma saati 9'dan büyükse ekstra çalışma saati verimliliğe eklenir
                {
                    float additionalHours = workingHours - maxWorkingHours;
                    efficiency += (additionalHours / maxWorkingHours) * 100f;
                }
            }
            else //çalışma saati 0 ile 9 arasındaysa verimlilik çalışma saatinin 9'a bölümü ile hesaplanır
            {
                efficiency = (workingHours / maxWorkingHours) * 100f;
            }

            return efficiency;
        }

        /// <summary>
        /// Hafta, ay ve yıl için verimlilik hesaplama
        /// </summary>
        /// <param name="totalWorkingHours">çalışma saati</param>
        /// <param name="dayCount">çalışılan gün sayısı</param>
        /// <returns>verimlilik değeri</returns>
        public float Calculate(float totalWorkingHours, int dayCount)
        {
            float maxWorkingHours = 9f * dayCount;
            float efficiency = 0f;

            if (totalWorkingHours <= 0) //çalışma saati 0'dan küçükse verimlilik 0
            {
                efficiency = 0f;
            }
            else if (totalWorkingHours >= maxWorkingHours) //çalışma saati toplam çalışma saatinden büyük veya eşitse verimlilik 100
            {
                efficiency = 100f;

                if (totalWorkingHours > maxWorkingHours) //çalışma saati toplam çalışma saatinden büyükse ekstra çalışma saati verimliliğe eklenir
                {
                    float additionalHours = totalWorkingHours - maxWorkingHours;
                    efficiency += (additionalHours / maxWorkingHours) * 100f;
                }
            }
            else //çalışma saati 0 ile toplam çalışma saati arasındaysa verimlilik çalışma saatinin toplam çalışma saatiye bölümü ile hesaplanır
            {
                efficiency = (totalWorkingHours / maxWorkingHours) * 100f;
            }

            return efficiency;
        }
    }
}
