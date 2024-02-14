using KB.EATS.DataAnalysis.Test.Models.Entities;

namespace KB.EATS.DataAnalysis.Test
{
    [Obsolete("Bu sınıf oluşturulan örnek verilerin veri tabanına kaydedilmesi ve kaydedilen verileririn analiz edilmesi için kullanılmıştır. Artık kullanılmamaktadır.", false)]
    public class Analyze
    {
        private KbEatsContext db = new KbEatsContext();


        //Veritabınana örnek verilerin kaydı için gerekli yıl ay hafta gün verileri oluşturuldu ve gekli formata getirildi
        public void Prepare()
        {
            foreach (var user in db.Users.ToList())
            {
                //db.UserStatistics.Add(new UserStatistic
                //{
                //    UserId = user.UserId,
                //    UserStatisticNavigation = user
                //});
                //db.SaveChanges();

                //user.UserShifts = db.UserShifts.Where(x => x.UserId == user.UserId).ToList();
                //user.UserStatistic = db.UserStatistics.FirstOrDefault(x => x.UserId == user.UserId);
                //user.UserStatistic.Years = db.Years.Where(x => x.UserStatisticId == user.UserStatistic.UserStatisticId).ToList();
                //foreach (var shift in user.UserShifts.ToList())
                //{
                //    if (!user.UserStatistic.Years.Any(x => x.YearValue == shift.Date.Value.Year))
                //    {
                //        user.UserStatistic.Years.Add(new Year
                //        {
                //            YearValue = Convert.ToInt16(shift.Date.Value.Year)
                //        });
                //        db.SaveChanges();
                //    }
                //}

                //foreach (var year in user.UserStatistic.Years.ToList())
                //{
                //    for (int i = 1; i <= 12; i++)
                //    {
                //        year.Months.Add(new Month { MonthValue = Convert.ToByte(i) });
                //        db.SaveChanges();
                //    }
                //}

                //foreach (var year in user.UserStatistic.Years.ToList())
                //{
                //    year.Months = db.Months.Where(x => x.YearsId == year.YearsId).ToList();
                //    foreach (var month in year.Months.ToList())
                //    {
                //        int days = DateTime.DaysInMonth(year.YearValue.Value, month.MonthValue.Value);
                //float weekCount = days / 7f;
                //if (weekCount > 4)
                //    weekCount = 5;
                //else
                //    weekCount = 4;

                //for (int i = 1; i <= weekCount; i++)
                //{
                //    month.Weeks.Add(new Week { WeekValue = Convert.ToByte(i) });
                //    db.SaveChanges();
                //}

                //month.Weeks = db.Weeks.Where(x => x.MonthId == month.MonthId).ToList();
                //for (int i = 1; i <= days; i++)
                //{
                //    if (i <= 7)
                //    {
                //        month.Weeks.FirstOrDefault(x => x.WeekValue == 1).Days.Add(new Day { DayValue = Convert.ToByte(i) });
                //        db.SaveChanges();
                //    }
                //    else if (i > 7 && i <= 14)
                //    {
                //        month.Weeks.FirstOrDefault(x => x.WeekValue == 2).Days.Add(new Day { DayValue = Convert.ToByte(i) });
                //        db.SaveChanges();
                //    }
                //    else if (i > 14 && i <= 21)
                //    {
                //        month.Weeks.FirstOrDefault(x => x.WeekValue == 3).Days.Add(new Day { DayValue = Convert.ToByte(i) });
                //        db.SaveChanges();
                //    }
                //    else if (i > 21 && i <= 28)
                //    {
                //        month.Weeks.FirstOrDefault(x => x.WeekValue == 4).Days.Add(new Day { DayValue = Convert.ToByte(i) });
                //        db.SaveChanges();
                //    }
                //    else
                //    {
                //        month.Weeks.FirstOrDefault(x => x.WeekValue == 5).Days.Add(new Day { DayValue = Convert.ToByte(i) });
                //        db.SaveChanges();
                //    }
                //}
                //    }
                //}

                //foreach (var day in db.Days.ToList())
                //{
                //    day.IsAbsenteeism = true;
                //    day.IsCalculated = false;
                //}
                //db.SaveChanges();

                //var years = db.Years.Where(x => x.YearValue == 2021).ToList();
                //foreach (var year in years)
                //{
                //    year.Months = db.Months.Where(x => x.YearsId == year.YearsId && x.MonthValue > 3).ToList();
                //    foreach (var month in year.Months)
                //    {
                //        month.Weeks = db.Weeks.Where(x => x.MonthId == month.MonthId).ToList();
                //        foreach (var week in month.Weeks)
                //        {
                //            week.Days = db.Days.Where(x => x.WeekId == week.WeekId).ToList();
                //            foreach (var day in week.Days)
                //            {
                //                day.IsAbsenteeism = false;
                //                day.IsCalculated = false;
                //            }
                //        }
                //    }
                //}
                //db.SaveChanges();

                //foreach (var year in db.Years.ToList())
                //{
                //    int y = year.YearValue.Value;
                //    year.Months = db.Months.Where(x => x.YearsId == year.YearsId).ToList();
                //    foreach (var month in year.Months)
                //    {
                //        int m = month.MonthValue.Value;
                //        month.Weeks = db.Weeks.Where(x => x.MonthId == month.MonthId).ToList();
                //        foreach (var week in month.Weeks)
                //        {
                //            week.Days = db.Days.Where(x => x.WeekId == week.WeekId).ToList();
                //            foreach (var day in week.Days)
                //            {
                //                int d = day.DayValue.Value;

                //                DateTime date = new DateTime(y, m, d);
                //                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                //                {
                //                    day.IsAbsenteeism = false;
                //                    day.IsCalculated = false;
                //                }
                //            }
                //        }
                //    }
                //}
                //db.SaveChanges();

                //foreach (var day in db.Days.ToList())
                //{
                //    if (day.IsAbsenteeism == true && day.IsCalculated == false)
                //    {
                //        day.IsCalculated = true;
                //    }
                //}
                //db.SaveChanges();
            }
        }

        public void Start()
        {
            #region Days Calculation
            //foreach (var user in db.Users.ToList())
            //{

            //    List<AnalyzedDayModel> dayData = new List<AnalyzedDayModel>();

            //    user.UserStatistic = db.UserStatistics.FirstOrDefault(x => x.UserId == user.UserId);
            //    user.UserShifts = db.UserShifts.Where(x => x.UserId == user.UserId).ToList();
            //    user.UserStatistic.Years = db.Years.Where(x => x.UserStatisticId == user.UserStatistic.UserStatisticId).ToList();

            //    foreach (var shift in user.UserShifts.ToList())
            //    {
            //        var dataList = db.ShiftDatas.Where(x => x.UserShiftId == shift.UserShiftId).ToList();
            //        DayAnalysisModel dayAnalysisModel = new DayAnalysisModel();
            //        dayAnalysisModel.UserId = shift.UserId;
            //        dayAnalysisModel.ShiftDate = shift.Date.Value;
            //        dayAnalysisModel.EntryDateTime = dataList[0].FullDate.Value;
            //        dayAnalysisModel.ExitDateTime = dataList[1].FullDate.Value;
            //        dayAnalysisModel.EntryTime = dataList[0].Time.Value;
            //        dayAnalysisModel.ExitTime = dataList[1].Time.Value;

            //        DailyAnalysis dailyAnalysis = new DailyAnalysis(dayAnalysisModel);
            //        AnalyzedDayModel response = dailyAnalysis.Analyze();
            //        dayData.Add(response);
            //    }

            //    Console.WriteLine($"###### Data Count: {dayData.Count.ToString()} ######");
            //    int counter = 0;
            //    foreach (var data in dayData)
            //    {
            //        counter++;
            //        var year = user.UserStatistic.Years.FirstOrDefault(x => x.YearValue == data.EntryTime.Year);
            //        year.Months = db.Months.Where(x => x.YearsId == year.YearsId).ToList();

            //        var month = year.Months.FirstOrDefault(x => x.MonthValue == data.EntryTime.Month);
            //        month.Weeks = db.Weeks.Where(x => x.MonthId == month.MonthId).ToList();

            //        var week = month.Weeks.FirstOrDefault(x => x.WeekValue == (data.EntryTime.Day <= 7 ? 1 : data.EntryTime.Day > 7 && data.EntryTime.Day <= 14 ? 2 : data.EntryTime.Day > 14 && data.EntryTime.Day <= 21 ? 3 : data.EntryTime.Day > 21 && data.EntryTime.Day <= 28 ? 4 : 5));
            //        week.Days = db.Days.Where(x => x.WeekId == week.WeekId).ToList();

            //        Day analyseDay = week.Days.FirstOrDefault(x => x.DayValue == data.EntryTime.Day);
            //        analyseDay.EntryTime = data.EntryTime;
            //        analyseDay.ExitTime = data.ExitTime;
            //        analyseDay.WorkingHours = data.WorkingHours;
            //        analyseDay.Overtime = data.Overtime;
            //        analyseDay.EmployeeEfficiency = data.EmployeeEfficiency;
            //        analyseDay.Rank = data.Rank;
            //        analyseDay.IsAbsenteeism = data.IsAbsenteeism;
            //        analyseDay.IsCalculated = data.IsCalculated;

            //        Console.WriteLine($"###### Complated: #{counter.ToString()} ######");
            //    }
            //    db.SaveChanges();
            //    Console.WriteLine($"###### Saved ######");

            //}
            #endregion

            //List<Day> days = new List<Day>();
            //foreach (var us in db.UserStatistics.ToList())
            //{
            //    us.Years = db.Years.Where(x => x.UserStatisticId == us.UserStatisticId).ToList();
            //    foreach (var year in us.Years.ToList())
            //    {
            //        year.Months = db.Months.Where(x => x.YearsId == year.YearsId).ToList();
            //        foreach (var month in year.Months.ToList())
            //        {
            //            month.Weeks = db.Weeks.Where(x => x.MonthId == month.MonthId).ToList();
            //            foreach (var week in month.Weeks.ToList())
            //            {
            //                week.Days = db.Days.Where(x => x.WeekId == week.WeekId).ToList();
            //                foreach (var day in week.Days.ToList())
            //                {
            //                    days.Add(day);
            //                }
            //            }
            //        }
            //    }
            //}

            DateTime startDate = new DateTime(2020, 1, 1);
            DateTime EndDate = new DateTime(2021, 3, 31);

            #region Days Rank

            //List<DataList> dataList = new List<DataList>();
            //foreach (var day in days)
            //{
            //    dataList.Add(new DataList
            //    {
            //        UserID = day.Week.Month.Years.UserStatistic.UserId,
            //        YearID = day.Week.Month.Years.YearsId,
            //        YearValue = day.Week.Month.Years.YearValue.Value,
            //        MonthID = day.Week.Month.MonthId,
            //        MonthValue = day.Week.Month.MonthValue.Value,
            //        WeekID = day.Week.WeekId,
            //        WeekValue = day.Week.WeekValue.Value,
            //        DayID = day.DayId,
            //        DayValue = day.DayValue.Value,
            //        WorkingHours = day.WorkingHours,
            //        Rank = day.Rank
            //    });
            //}

            //for (DateTime date = startDate; date <= EndDate; date = date.AddDays(1))
            //{
            //    if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
            //    {
            //        var data = dataList.Where(x => x.YearValue == date.Year && x.MonthValue == date.Month && x.DayValue == date.Day).ToList();
            //        List<RankingCalculationModel> model = new List<RankingCalculationModel>();
            //        foreach (var item in data)
            //        {
            //            model.Add(new RankingCalculationModel
            //            {
            //                ID = item.DayID,
            //                WorkingHours = item.WorkingHours != null ? item.WorkingHours.Value : 0,
            //            });
            //        }
            //        RankingCalculation rc = new RankingCalculation();
            //        var result = rc.Calculate(model);
            //        foreach (var item in result)
            //        {
            //            db.Days.Find(item.ID).Rank = item.Rank;
            //        }
            //        db.SaveChanges();

            //    }
            //}
            #endregion

            #region Hafta Analizi
            //foreach (var us in db.UserStatistics.ToList())
            //{
            //    us.Years = db.Years.Where(x => x.UserStatisticId == us.UserStatisticId).ToList();
            //    foreach (var year in us.Years.ToList())
            //    {
            //        year.Months = db.Months.Where(x => x.YearsId == year.YearsId).ToList();
            //        foreach (var month in year.Months.ToList())
            //        {
            //            month.Weeks = db.Weeks.Where(x => x.MonthId == month.MonthId).ToList();
            //            foreach (var week in month.Weeks.ToList())
            //            {
            //                week.Days = db.Days.Where(x => x.WeekId == week.WeekId).ToList();
            //                week.TotalWorkingHours = 0;
            //                week.TotalOvertime = 0;
            //                week.TotalAbsenteeism = 0;
            //                week.EmployeeEfficiency = 0;
            //                week.Rank = 0;
            //                foreach (var day in week.Days.ToList())
            //                {
            //                    week.TotalWorkingHours += day.WorkingHours != null ? day.WorkingHours.Value : 0;
            //                    week.TotalOvertime += day.Overtime != null ? day.Overtime.Value : 0;
            //                    week.TotalAbsenteeism += day.IsAbsenteeism != null ? (day.IsAbsenteeism.Value == true ? (byte)1 : (byte)0) : (byte)0;
            //                }
            //                EfficiencyCalculation ec = new EfficiencyCalculation();
            //                week.EmployeeEfficiency = ec.Calculate(Convert.ToSingle(week.TotalWorkingHours.Value), week.Days.Count);
            //                week.IsCalculated = true;
            //                db.SaveChanges();
            //            }
            //        }
            //    }
            //}

            //db.Weeks.Where(x => x.IsCalculated == true && x.TotalAbsenteeism == 0 && x.TotalWorkingHours == 0 && x.TotalOvertime == 0 && x.EmployeeEfficiency == 0).ToList().ForEach(x =>
            //{
            //    x.IsCalculated = false;
            //    db.SaveChanges();
            //});

            #endregion

            #region Hafta Sıralaması
            //List<Week> weeks = new List<Week>();
            //foreach (var us in db.UserStatistics.ToList())
            //{
            //    us.Years = db.Years.Where(x => x.UserStatisticId == us.UserStatisticId).ToList();
            //    foreach (var year in us.Years.ToList())
            //    {
            //        year.Months = db.Months.Where(x => x.YearsId == year.YearsId).ToList();
            //        foreach (var month in year.Months.ToList())
            //        {
            //            month.Weeks = db.Weeks.Where(x => x.MonthId == month.MonthId).ToList();
            //            foreach (var week in month.Weeks.ToList())
            //            {
            //                weeks.Add(week);
            //            }
            //        }
            //    }
            //}

            //List<DataList> dataList = new List<DataList>();
            //foreach (var week in weeks)
            //{
            //    dataList.Add(new DataList
            //    {
            //        UserID = week.Month.Years.UserStatistic.UserId,
            //        YearID = week.Month.Years.YearsId,
            //        YearValue = week.Month.Years.YearValue.Value,
            //        MonthID = week.Month.MonthId,
            //        MonthValue = week.Month.MonthValue.Value,
            //        WeekID = week.WeekId,
            //        WeekValue = week.WeekValue.Value,
            //        DayID = 0,
            //        DayValue = 0,
            //        WorkingHours = week.TotalWorkingHours,
            //        Rank = week.Rank
            //    });
            //}

            //for (DateTime date = startDate; date <= EndDate; date = date.AddDays(1))
            //{
            //    if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
            //    {
            //        int w = 0;
            //        if (date.Day <= 7)
            //        {
            //            w = 1;
            //        }
            //        else if (date.Day > 7 && date.Day <= 14)
            //        {
            //            w = 2;
            //        }
            //        else if (date.Day > 14 && date.Day <= 21)
            //        {
            //            w = 3;
            //        }
            //        else if (date.Day > 21 && date.Day <= 28)
            //        {
            //            w = 4;
            //        }
            //        else
            //        {
            //            w = 5;
            //        }


            //        var data = dataList.Where(x => x.YearValue == date.Year && x.MonthValue == date.Month && x.WeekValue == w).ToList();
            //        List<RankingCalculationModel> model = new List<RankingCalculationModel>();
            //        foreach (var item in data)
            //        {
            //            model.Add(new RankingCalculationModel
            //            {
            //                ID = item.WeekID,
            //                WorkingHours = item.WorkingHours != null ? item.WorkingHours.Value : 0,
            //            });
            //        }
            //        RankingCalculation rc = new RankingCalculation();
            //        var result = rc.Calculate(model);
            //        foreach (var item in result)
            //        {
            //            var week = db.Weeks.FirstOrDefault(x => x.WeekId == item.ID && x.Rank == 0 && x.IsCalculated == true);
            //            if (week != null)
            //            {
            //                week.Rank = item.Rank;
            //            }
            //        }
            //        db.SaveChanges();

            //    }
            //}
            #endregion

            #region Ay Analizi

            //foreach (var us in db.UserStatistics.ToList())
            //{
            //    us.Years = db.Years.Where(x => x.UserStatisticId == us.UserStatisticId).ToList();
            //    foreach (var year in us.Years.ToList())
            //    {
            //        year.Months = db.Months.Where(x => x.YearsId == year.YearsId).ToList();
            //        foreach (var month in year.Months.ToList())
            //        {
            //            month.Weeks = db.Weeks.Where(x => x.MonthId == month.MonthId).ToList();
            //            month.TotalAbsenteeism = 0;
            //            month.TotalWorkingHours = 0;
            //            month.TotalOvertime = 0;
            //            month.EmployeeEfficiency = 0;
            //            month.Rank = 0;
            //            foreach (var week in month.Weeks.ToList())
            //            {
            //                month.TotalAbsenteeism += week.TotalAbsenteeism != null ? week.TotalAbsenteeism.Value : (byte)0;
            //                month.TotalWorkingHours += week.TotalWorkingHours != null ? week.TotalWorkingHours.Value : 0;
            //                month.TotalOvertime += week.TotalOvertime != null ? week.TotalOvertime.Value : 0;
            //            }
            //            EfficiencyCalculation ec = new EfficiencyCalculation();
            //            month.EmployeeEfficiency = ec.Calculate(Convert.ToSingle(month.TotalWorkingHours.Value), month.Weeks.Count);
            //            month.IsCalculated = true;
            //            db.SaveChanges();
            //        }
            //    }
            //}

            //db.Months.Where(x => x.IsCalculated == true && x.TotalAbsenteeism == 0 && x.TotalWorkingHours == 0 && x.TotalOvertime == 0 && x.EmployeeEfficiency == 0).ToList().ForEach(x =>
            //{
            //    x.IsCalculated = false;
            //    db.SaveChanges();
            //});
            #endregion

            #region Ay Sıralaması
            //List<Month> months = new List<Month>();
            //foreach (var us in db.UserStatistics.ToList())
            //{
            //    us.Years = db.Years.Where(x => x.UserStatisticId == us.UserStatisticId).ToList();
            //    foreach (var year in us.Years.ToList())
            //    {
            //        year.Months = db.Months.Where(x => x.YearsId == year.YearsId).ToList();
            //        foreach (var month in year.Months.ToList())
            //        {
            //            months.Add(month);
            //        }
            //    }
            //}

            //List<DataList> dataList = new List<DataList>();
            //foreach (var month in months)
            //{
            //    dataList.Add(new DataList
            //    {
            //        UserID = month.Years.UserStatistic.UserId,
            //        YearID = month.Years.YearsId,
            //        YearValue = month.Years.YearValue.Value,
            //        MonthID = month.MonthId,
            //        MonthValue = month.MonthValue.Value,
            //        WeekID = 0,
            //        WeekValue = 0,
            //        DayID = 0,
            //        DayValue = 0,
            //        WorkingHours = month.TotalWorkingHours,
            //        Rank = month.Rank
            //    });
            //}

            //for (DateTime date = startDate; date <= EndDate; date = date.AddDays(1))
            //{
            //    if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
            //    {
            //        int w = 0;
            //        if (date.Day <= 7)
            //        {
            //            w = 1;
            //        }
            //        else if (date.Day > 7 && date.Day <= 14)
            //        {
            //            w = 2;
            //        }
            //        else if (date.Day > 14 && date.Day <= 21)
            //        {
            //            w = 3;
            //        }
            //        else if (date.Day > 21 && date.Day <= 28)
            //        {
            //            w = 4;
            //        }
            //        else
            //        {
            //            w = 5;
            //        }


            //        var data = dataList.Where(x => x.YearValue == date.Year && x.MonthValue == date.Month).ToList();
            //        List<RankingCalculationModel> model = new List<RankingCalculationModel>();
            //        foreach (var item in data)
            //        {
            //            model.Add(new RankingCalculationModel
            //            {
            //                ID = item.MonthID,
            //                WorkingHours = item.WorkingHours != null ? item.WorkingHours.Value : 0,
            //            });
            //        }
            //        RankingCalculation rc = new RankingCalculation();
            //        var result = rc.Calculate(model);
            //        foreach (var item in result)
            //        {
            //            var month = db.Months.FirstOrDefault(x => x.MonthId == item.ID && x.IsCalculated == true);
            //            if (month != null)
            //            {
            //                month.Rank = item.Rank;
            //            }
            //        }
            //        db.SaveChanges();

            //    }
            //}
            #endregion

            #region Yıl Analizi
            //foreach (var us in db.UserStatistics.ToList())
            //{
            //    us.Years = db.Years.Where(x => x.UserStatisticId == us.UserStatisticId).ToList();
            //    foreach (var year in us.Years.ToList())
            //    {
            //        year.Months = db.Months.Where(x => x.YearsId == year.YearsId).ToList();
            //        year.TotalAbsenteeism = 0;
            //        year.TotalWorkingHours = 0;
            //        year.TotalOvertime = 0;
            //        year.EmployeeEfficiency = 0;
            //        year.Rank = 0;
            //        foreach (var month in year.Months.ToList())
            //        {
            //            year.TotalAbsenteeism += month.TotalAbsenteeism != null ? month.TotalAbsenteeism.Value : (byte)0;
            //            year.TotalWorkingHours += month.TotalWorkingHours != null ? month.TotalWorkingHours.Value : 0;
            //            year.TotalOvertime += month.TotalOvertime != null ? month.TotalOvertime.Value : 0;
            //        }
            //        EfficiencyCalculation ec = new EfficiencyCalculation();
            //        year.EmployeeEfficiency = ec.Calculate(Convert.ToSingle(year.TotalWorkingHours.Value), year.Months.Count);
            //        year.IsCalculated = true;
            //        db.SaveChanges();
            //    }
            //}
            #endregion

            #region Yıl Sıralaması
            //List<Year> years = new List<Year>();
            //foreach (var us in db.UserStatistics.ToList())
            //{
            //    us.Years = db.Years.Where(x => x.UserStatisticId == us.UserStatisticId).ToList();
            //    foreach (var year in us.Years.ToList())
            //    {
            //        years.Add(year);
            //    }
            //}

            //List<DataList> dataList = new List<DataList>();
            //foreach (var year in years)
            //{
            //    dataList.Add(new DataList
            //    {
            //        UserID = year.UserStatistic.UserId,
            //        YearID = year.YearsId,
            //        YearValue = year.YearValue.Value,
            //        MonthID = 0,
            //        MonthValue = 0,
            //        WeekID = 0,
            //        WeekValue = 0,
            //        DayID = 0,
            //        DayValue = 0,
            //        WorkingHours = year.TotalWorkingHours,
            //        Rank = year.Rank
            //    });
            //}

            //for (DateTime date = startDate; date <= EndDate; date = date.AddDays(1))
            //{
            //    if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
            //    {
            //        int w = 0;
            //        if (date.Day <= 7)
            //        {
            //            w = 1;
            //        }
            //        else if (date.Day > 7 && date.Day <= 14)
            //        {
            //            w = 2;
            //        }
            //        else if (date.Day > 14 && date.Day <= 21)
            //        {
            //            w = 3;
            //        }
            //        else if (date.Day > 21 && date.Day <= 28)
            //        {
            //            w = 4;
            //        }
            //        else
            //        {
            //            w = 5;
            //        }


            //        var data = dataList.Where(x => x.YearValue == date.Year).ToList();
            //        List<RankingCalculationModel> model = new List<RankingCalculationModel>();
            //        foreach (var item in data)
            //        {
            //            model.Add(new RankingCalculationModel
            //            {
            //                ID = item.YearID,
            //                WorkingHours = item.WorkingHours != null ? item.WorkingHours.Value : 0,
            //            });
            //        }
            //        RankingCalculation rc = new RankingCalculation();
            //        var result = rc.Calculate(model);
            //        foreach (var item in result)
            //        {
            //            var year = db.Years.FirstOrDefault(x => x.YearsId == item.ID && x.IsCalculated == true);
            //            if (year != null)
            //            {
            //                year.Rank = item.Rank;
            //            }
            //        }
            //        db.SaveChanges();

            //    }
            //}
            #endregion
        }
    }
}


/// <summary>
/// veri atabanına eklenmiş örnek verilerin analizi sırasında kullanılan veri modeli
/// </summary>
class DataList
{
    public int UserID { get; set; }
    public int YearID { get; set; }
    public int YearValue { get; set; }
    public int MonthID { get; set; }
    public int MonthValue { get; set; }
    public int WeekID { get; set; }
    public int WeekValue { get; set; }
    public int DayID { get; set; }
    public int DayValue { get; set; }
    public double? WorkingHours { get; set; }
    public byte? Rank { get; set; }
}
