using KB.EATS.DataAnalysis.Models;

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


            DateTime date = new DateTime(2023, 11, 13);
            int val = GetWeekNumberOfMonth(date);

            Console.ReadKey();
        }

        static int GetWeekNumberOfMonth(DateTime date)
        {
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);

            // Hesaplama için pazartesi gününü referans alıyoruz
            DayOfWeek firstDayOfWeek = DayOfWeek.Monday;

            // İlk günün haftasını alıyoruz
            int weekNumber = (date.Day + GetDayOfWeekOffset(firstDayOfMonth, firstDayOfWeek) - 1) / 7 + 1;

            return weekNumber;
        }

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