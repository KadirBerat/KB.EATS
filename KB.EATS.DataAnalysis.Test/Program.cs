namespace KB.EATS.DataAnalysis.Test
{
    /// <summary>
    /// DataAnalysis kütüphanesinin testi için kullanılıyor
    /// </summary>
    internal class Program
    {

        private static string FolderPath = @$"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName}\SampleData";
        private static string FilePath = @$"{FolderPath}\27-10-2023_14-45-52_Modified.sampledata";

        static void Main(string[] args)
        {
            //SampleDataConversion sampleDataConversion = new SampleDataConversion(FilePath);
            //List<Models.ShiftDataModel> data = sampleDataConversion.Get();
            //List<ShiftDataModelSimplified> simplifiedData = sampleDataConversion.GetSimplified();

            //WorkingHourCalculation whc = new WorkingHourCalculation(simplifiedData);
            //List<CalculatedShiftDataModel> calculatedModle = whc.Get();


            //DateTime date = new DateTime(2023, 11, 13);
            //int val = GetWeekNumberOfMonth(date);

            //TimeSpan en = new TimeSpan(9, 15, 0);
            //TimeSpan ex = new TimeSpan(17, 5, 0);

            //TimeSpan wh = ex - en;

            //EfficiencyCalculation ec = new EfficiencyCalculation();
            //float a1 = ec.Calculate(0);
            //float a2 = ec.Calculate(0.2f);
            //float a3 = ec.Calculate(2);
            //float a4 = ec.Calculate(3);
            //float a5 = ec.Calculate(6);
            //float a6 = ec.Calculate(8.4f);
            //float a7 = ec.Calculate(9);
            //float a8 = ec.Calculate(9.5f);
            //float a9 = ec.Calculate(12);
            //float a10 = ec.Calculate(15);
            //float a11 = ec.Calculate(18);
            //float a12 = ec.Calculate(20);

            //List<RankingCalculationModel> model = new List<RankingCalculationModel>();
            //model.Add(new RankingCalculationModel() { ID = 1, WorkingHours = 8.5f });
            //model.Add(new RankingCalculationModel() { ID = 1, WorkingHours = 9.5f });
            //model.Add(new RankingCalculationModel() { ID = 1, WorkingHours = 15.5f });

            //var result = model.OrderByDescending(x => x.WorkingHours).ToList();
            //for (int i = 0; i < result.Count; i++)
            //{
            //    result[i].Rank = (short)(i + 1);
            //}

            Analyze analyze = new Analyze();
            //analyze.Prepare();
            analyze.Start();

            //float wc = 31 / 7f;

            Console.WriteLine("----- Completed! -----");
            Console.ReadKey();
        }

        //ObsoleteAttribute(String, Boolean) veya sadeleştirilmiş haliyle Obsolete(String, Boolean) bir methodun artık kullanılmadığını belirtmek için kullanılır. Buradaki örneklerde string ile kullaıcıya gösterilecek mesaj ve boolean ile de bu methodun kullanılması durumunda programın hata verip vermeyeceği belirtilmiştir.

        [Obsolete("Bu method test için kullanılmıştır. Artık kullanılmamaktadır.", false)]
        static int GetWeekNumberOfMonth(DateTime date)
        {
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);

            // Hesaplama için pazartesi gününü referans alıyoruz
            DayOfWeek firstDayOfWeek = DayOfWeek.Monday;

            // İlk günün haftasını alıyoruz
            int weekNumber = (date.Day + GetDayOfWeekOffset(firstDayOfMonth, firstDayOfWeek) - 1) / 7 + 1;

            return weekNumber;
        }

        [Obsolete("Bu method test için kullanılmıştır. Artık kullanılmamaktadır.", false)]
        static int GetDayOfWeekOffset(DateTime date, DayOfWeek firstDayOfWeek)
        {
            int offset = date.DayOfWeek - firstDayOfWeek;
            if (offset < 0)
            {
                // Haftanın başından önceki günleri bir önceki haftaya dahil etmek için
                offset += 7;
            }
            return offset;
        }
    }
}