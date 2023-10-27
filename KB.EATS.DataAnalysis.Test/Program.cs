using KB.EATS.DataAnalysis.Models;

namespace KB.EATS.DataAnalysis.Test
{
    internal class Program
    {
        private static string FolderPath = @$"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName}\SampleData";
        private static string FilePath = @$"{FolderPath}\27-10-2023_14-45-52_Modified.sampledata";

        static void Main(string[] args)
        {
            SampleDataConversion sampleDataConversion = new SampleDataConversion(FilePath);
            List<Models.ShiftDataModel> data = sampleDataConversion.Get();
            List<ShiftDataModelSimplified> simplifiedData = sampleDataConversion.GetSimplified();
            Console.ReadKey();
        }
    }
}