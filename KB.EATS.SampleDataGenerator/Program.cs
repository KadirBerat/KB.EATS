using KB.EATS.DataAnalysis;
using KB.EATS.DataAnalysis.Models;
using KB.EATS.SampleDataGenerator.Models;
using System.Diagnostics;
using System.IO.Enumeration;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KB.EATS.SampleDataGenerator
{
    internal class Program
    {
        private static Random random = new Random();
        private static string FolderPath = @$"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName}\SampleData";
        private static string sampledataFileName = "";
        private static KbEatsContext db = new KbEatsContext();

        static void Main(string[] args)
        {
            //Çalışanlar
            string[] employees = new string[] { "Employee-1", "Employee-2", "Employee-3", "Employee-4", "Employee-5", "Employee-6", "Employee-7", "Employee-8", "Employee-9", "Employee-10", "Employee-11", "Employee-12", "Employee-13", "Employee-14", "Employee-15" };

            List<string> sampleData = new List<string>();
            DateTime startDate = new DateTime(2020, 1, 1);
            DateTime EndDate = new DateTime(2021, 3, 31);

            int anomaly = random.Next(100, 500);
            List<int> anomalies = DataSelector(1, 9780, anomaly);

            //List<int> anomalies = new List<int>();
            //while (anomalies.Count < anomaly)
            //{
            //    int rastgeleSayi = random.Next(min, max + 1);

            //    if (!anomalies.Contains(rastgeleSayi))
            //    {
            //        anomalies.Add(rastgeleSayi);
            //    }
            //}
            //anomalies.Sort();

            #region Fisher–Yates shuffle
            //Sıra verisi
            //List<int> sequence = new List<int>();
            //for (int i = 0; i <= 14; i++)
            //{
            //    sequence.Add(i);
            //}

            //Fisher–Yates shuffle
            //int n = sequence.Count;
            //for (int i = n - 1; i > 0; i--)
            //{
            //    int r = random.Next(i + 1);
            //    int temp = sequence[i];
            //    sequence[i] = sequence[r];
            //    sequence[r] = temp;
            //}

            //foreach (int num in sequence)
            //{
            //    Console.WriteLine(num);
            //} 
            #endregion

            #region LINQ
            List<int> sequence = CreateSequence();

            //foreach (var num in sequence)
            //{
            //    Console.WriteLine(num);
            //}
            #endregion

            #region Zaman Aralıkları
            //Giriş
            DateTime shiftStartM15 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7, 45, 0);
            DateTime shiftStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
            DateTime shiftStartP15 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 15, 0);

            //Çıkış
            DateTime shiftEndM15 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 45, 0);
            DateTime shiftEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0);
            DateTime shiftEndP15 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 15, 0);

            //Anomali - geç kalma
            DateTime shiftStartP60 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0);
            DateTime shiftStartP90 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 30, 0);
            DateTime shiftStartP120 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0);

            //anomali - erken çıkma
            DateTime shiftEndM60 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 00, 0);
            DateTime shiftEndM90 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 30, 0);
            DateTime shiftEndM120 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 00, 0);
            #endregion

            int dayCounter = 0;

            int counter = 0;
            int counterAEn = 0;
            int counterAEx = 0;

            for (DateTime date = startDate; date <= EndDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    //giriş verisi
                    sequence = CreateSequence();
                    foreach (int employee in sequence)
                    {
                        string data = $"{date.ToString("dd-MM-yyyy")}_";
                        if (anomalies.Contains(counter))
                        {
                            DateTime entryA;
                            int selectedAnomaly = random.Next(3);
                            switch (selectedAnomaly)
                            {
                                case 1:
                                    entryA = RandomTimeBetween(shiftStartP15, shiftStartP60);
                                    break;
                                case 2:
                                    entryA = RandomTimeBetween(shiftStartP15, shiftStartP90);
                                    break;
                                case 3:
                                    entryA = RandomTimeBetween(shiftStartP15, shiftStartP120);
                                    break;
                                default:
                                    entryA = RandomTimeBetween(shiftStartP15, shiftStartP60);
                                    break;
                            }
                            data += $"{entryA.ToString("HH-mm-ss")}_{employees[employee].ToString()}";
                            counterAEn++;
                        }
                        else
                        {
                            DateTime entry = RandomTimeBetween(shiftStartM15, shiftStartP15);
                            data += $"{entry.ToString("HH-mm-ss")}_{employees[employee].ToString()}";
                        }
                        sampleData.Add(data);
                        counter++;
                    }

                    //çıkış verisi
                    sequence = CreateSequence();
                    foreach (int employee in sequence)
                    {
                        string data = $"{date.ToString("dd-MM-yyyy")}_";
                        if (anomalies.Contains(counter))
                        {
                            DateTime exitA;
                            int selectedAnomaly = random.Next(3);
                            switch (selectedAnomaly)
                            {
                                case 1:
                                    exitA = RandomTimeBetween(shiftEndM60, shiftEndM15);
                                    break;
                                case 2:
                                    exitA = RandomTimeBetween(shiftEndM90, shiftEndM15);
                                    break;
                                case 3:
                                    exitA = RandomTimeBetween(shiftEndM120, shiftEndM15);
                                    break;
                                default:
                                    exitA = RandomTimeBetween(shiftEndM60, shiftEndM15);
                                    break;
                            }
                            data += $"{exitA.ToString("HH-mm-ss")}_{employees[employee].ToString()}";
                            counterAEx++;
                        }
                        else
                        {
                            DateTime exit = RandomTimeBetween(shiftEndM15, shiftEndP15);
                            data += $"{exit.ToString("HH-mm-ss")}_{employees[employee].ToString()}";
                        }
                        sampleData.Add(data);
                        counter++;
                    }

                    dayCounter++;
                }
            }

            //foreach (int sayi in anomalies)
            //{
            //    Console.WriteLine(sayi);
            //}

            //Console.WriteLine(Environment.NewLine);

            Console.WriteLine($"Toplam Gün Sayısı: {dayCounter.ToString()}");
            Console.WriteLine($"Toplam Veri Sayısı: {sampleData.Count.ToString()}");
            Console.WriteLine($"Toplam Anomali Sayısı: {anomaly.ToString()}");
            Console.WriteLine($"Giriş Anomali Sayısı: {counterAEn.ToString()}");
            Console.WriteLine($"Çıkış Anomali Sayısı: {counterAEx.ToString()}");
            string modifiedFilePath = string.Empty;
            List<string> modifiedData = new List<string>();
            DirectoryControl();
            string filePath = FileControl(false);
            if (filePath != "#!")
            {
                File.WriteAllLines(filePath, sampleData.ToArray());

                SampleDataConversion sampleDataConversion = new SampleDataConversion(filePath);
                List<DataAnalysis.Models.ShiftDataModelSimplified> data = sampleDataConversion.GetSimplified();
                int p5 = (5 * data.Count) / 100;
                int p15 = (15 * data.Count) / 100;
                int absenteeism = random.Next(p5, p15);
                List<int> selectedAbsences = DataSelector(1, data.Count, absenteeism);

                counter = 0;
                foreach (var item in data)
                {
                    if (!selectedAbsences.Contains(counter))
                    {
                        string entryLineData = $"{item.Entry.FullDate.ToString("dd-MM-yyyy_HH-mm-ss")}_{item.Employee}";
                        string exitLineData = $"{item.Exit.FullDate.ToString("dd-MM-yyyy_HH-mm-ss")}_{item.Employee}";
                        modifiedData.Add(entryLineData);
                        modifiedData.Add(exitLineData);
                    }
                    counter++;
                }

                modifiedFilePath = FileControl(true);
                File.WriteAllLines(modifiedFilePath, modifiedData.ToArray());

                Console.WriteLine($"Toplam Devamsızlık Sayısı: {absenteeism}");
                Console.WriteLine($"Son Veri Sayısı: {modifiedData.Count().ToString()}");

                Console.WriteLine($"Son Veri Dosyası: {sampledataFileName}_Modified.sampledata");

            }

            Console.WriteLine("Tamamlandı!\n\n");

            Console.WriteLine("Veriler veritabanı'na kayıt edilsin mi? (Y/N)");
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Y)
            {
                foreach (var employee in employees)
                {
                    db.Users.Add(new User
                    {
                        FirstName = employee.Split('-')[0],
                        LastName = employee.Split("-")[1],
                        IsActive = true,
                        IsAdmin = false,
                        Password = "1234",
                        RegisterDate = DateTime.Now,
                        Username = employee
                    });
                }
                db.SaveChanges();

                SampleDataConversion sdc = new SampleDataConversion(modifiedFilePath);
                List<ShiftDataModelSimplified> modifiedDataList = sdc.GetSimplified();
                foreach (var data in modifiedDataList)
                {
                    db.Users.FirstOrDefault(x => x.Username == data.Employee).UserShifts.Add(new UserShift
                    {
                        Date = data.Date,
                        ShiftData = new List<ShiftData>()
                        {
                             new ShiftData {
                                 Date = data.Entry.Date.ToDateTime(data.Entry.Time),
                                 Time = data.Entry.Time.ToTimeSpan(),
                                 FullDate = data.Entry.FullDate
                             },
                             new ShiftData {
                                 Date = data.Exit.Date.ToDateTime(data.Exit.Time),
                                 Time = data.Exit.Time.ToTimeSpan(),
                                 FullDate = data.Exit.FullDate
                             }
                        }
                    });

                }
                db.SaveChanges();

                Console.WriteLine("Veriler veritabanı'na kayıt edildi!");
            }

            Console.ReadKey();
        }

        private static List<int> DataSelector(int min, int max, int count)
        {
            List<int> selectedDataList = new List<int>();
            while (selectedDataList.Count < count)
            {
                int rastgeleSayi = random.Next(min, max + 1);

                if (!selectedDataList.Contains(rastgeleSayi))
                {
                    selectedDataList.Add(rastgeleSayi);
                }
            }
            selectedDataList.Sort();
            return selectedDataList;
        }

        private static void DirectoryControl()
        {
            try
            {
                if (!Directory.Exists(FolderPath))
                    Directory.CreateDirectory(FolderPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static string FileControl(bool isModifiedFile)
        {
            if (isModifiedFile)
            {
                try
                {
                    string fileName = $"{sampledataFileName}_Modified.sampledata";
                    string filePath = $@"{FolderPath}\{fileName}";
                    bool fileCheck = File.Exists(filePath);
                    if (fileCheck == false)
                    {
                        File.Create(filePath).Close();
                    }
                    return filePath;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return "#!";
                }
            }
            else
            {
                try
                {
                    string date = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
                    sampledataFileName = date;
                    string fileName = $"{date}.sampledata";
                    string filePath = $@"{FolderPath}\{fileName}";
                    bool fileCheck = File.Exists(filePath);
                    if (fileCheck == false)
                    {
                        File.Create(filePath).Close();
                    }
                    return filePath;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return "#!";
                }
            }
        }

        static List<int> CreateSequence()
        {
            return Enumerable.Range(0, 15).OrderBy(x => Guid.NewGuid()).ToList();
        }

        //Rastgele zaman
        static DateTime RandomTimeBetween(DateTime start, DateTime end)
        {
            int totalMinutes = (int)(end - start).TotalMinutes;
            int randomMinutes = random.Next(totalMinutes);
            return start.AddMinutes(randomMinutes);
        }
    }
}