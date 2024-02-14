using KB.EATS.WebUI.Models;
using KB.EATS.WebUI.Models.Entities;
using KB.EATS.WebUI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace KB.EATS.WebUI.Controllers
{
    public class HomeController : Controller
    {
        //logger dependency injection
        private readonly ILogger<HomeController> _logger;

        //db context dependency injection
        private KbEatsContext db = new KbEatsContext();

        //logger dependency injection
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //mvc default index page
        public IActionResult Index()
        {
            return View();
        }

        //mvc default privacy page
        public IActionResult Privacy()
        {
            return View();
        }

        //mvc default error redirect page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Dashboard sayfasýný açan action
        /// </summary>
        /// <returns>Dashboard.cshtml + DashboardWiewModel</returns>
        public IActionResult Dashboard()
        {
            DashboardWiewModel model = new DashboardWiewModel();

            #region Çalýlanlarý modele ekliyorum
            model.Employees = new List<EmployeeData>();
            foreach (var employee in db.Users.ToList())
            {
                model.Employees.Add(new EmployeeData
                {
                    ID = employee.UserId,
                    Username = employee.Username,
                    FullName = employee.FirstName + " " + employee.LastName
                });
            }
            #endregion

            return View(model);
        }

        /// <summary>
        /// Seçilen çalýþanýn yýllarýný döndüren action
        /// </summary>
        /// <param name="id">Çalýþan id</param>
        /// <returns>_YearsSectionPartial.cshtml + YearsSectionPartialViewModel</returns>
        public PartialViewResult EmployeeSelect(int id)
        {
            YearsSectionPartialViewModel model = new YearsSectionPartialViewModel();
            model.Years = new List<YearData>();

            #region Seçili çalýþanýn yýllarýnýn istatistiklerini modele ekliyorum
            var years = db.Years.ToList();
            foreach (var year in years.Where(x => x.UserStatisticId == id).ToList())
            {
                //yýllarýn istatistilerinin eklenmesi
                List<YearGraphModel> overtimes = new List<YearGraphModel>();
                List<YearGraphModel> efficiency = new List<YearGraphModel>();
                List<YearGraphModel> workingHours = new List<YearGraphModel>();
                List<YearGraphModel> absenteeism = new List<YearGraphModel>();
                List<YearGraphModel> rank = new List<YearGraphModel>();
                foreach (var item in years.Where(x => x.YearValue == year.YearValue))
                {
                    overtimes.Add(new YearGraphModel
                    {
                        Value = Math.Round(item.TotalOvertime.Value, 2),
                        Username = db.Users.Find(item.UserStatisticId).Username,
                        Current = item.UserStatisticId == id ? true : false
                    });
                    efficiency.Add(new YearGraphModel
                    {
                        Value = Math.Round(item.EmployeeEfficiency.Value, 2),
                        Username = db.Users.Find(item.UserStatisticId).Username,
                        Current = item.UserStatisticId == id ? true : false
                    });
                    workingHours.Add(new YearGraphModel
                    {
                        Value = Math.Round(item.TotalWorkingHours.Value, 2),
                        Username = db.Users.Find(item.UserStatisticId).Username,
                        Current = item.UserStatisticId == id ? true : false
                    });
                    absenteeism.Add(new YearGraphModel
                    {
                        Value = item.TotalAbsenteeism.Value,
                        Username = db.Users.Find(item.UserStatisticId).Username,
                        Current = item.UserStatisticId == id ? true : false
                    });
                    rank.Add(new YearGraphModel
                    {
                        Value = item.Rank.Value,
                        Username = db.Users.Find(item.UserStatisticId).Username,
                        Current = item.UserStatisticId == id ? true : false
                    });
                }

                //yýllarýn istatistilerinin grafik verilerinin eklenmesi
                YearGraphData overtimeData = new YearGraphData();
                overtimeData.Labels = new string[overtimes.Count];
                overtimeData.Values = new double[overtimes.Count];
                overtimeData.Colors = new string[overtimes.Count];
                YearGraphData efficiencyData = new YearGraphData();
                efficiencyData.Labels = new string[efficiency.Count];
                efficiencyData.Values = new double[efficiency.Count];
                efficiencyData.Colors = new string[efficiency.Count];
                YearGraphData workingHoursData = new YearGraphData();
                workingHoursData.Labels = new string[workingHours.Count];
                workingHoursData.Values = new double[workingHours.Count];
                workingHoursData.Colors = new string[workingHours.Count];
                YearGraphData absenteeismData = new YearGraphData();
                absenteeismData.Labels = new string[absenteeism.Count];
                absenteeismData.Values = new double[absenteeism.Count];
                absenteeismData.Colors = new string[absenteeism.Count];
                YearGraphData rankData = new YearGraphData();
                rankData.Labels = new string[rank.Count];
                rankData.Values = new double[rank.Count];
                rankData.Colors = new string[rank.Count];
                for (int i = 0; i < overtimes.Count; i++)
                {
                    overtimeData.Labels[i] = overtimes[i].Username;
                    overtimeData.Values[i] = overtimes[i].Value;
                    if (overtimes[i].Current == true)
                    {
                        overtimeData.Colors[i] = "rgba(255, 99, 132, 0.8)";
                    }
                    else
                    {
                        overtimeData.Colors[i] = "rgba(54, 162, 235, 0.8)";
                    }
                    efficiencyData.Labels[i] = efficiency[i].Username;
                    efficiencyData.Values[i] = efficiency[i].Value;
                    if (efficiency[i].Current == true)
                    {
                        efficiencyData.Colors[i] = "rgba(255, 99, 132, 0.8)";
                    }
                    else
                    {
                        efficiencyData.Colors[i] = "rgba(54, 162, 235, 0.8)";
                    }
                    workingHoursData.Labels[i] = workingHours[i].Username;
                    workingHoursData.Values[i] = workingHours[i].Value;
                    if (workingHours[i].Current == true)
                    {
                        workingHoursData.Colors[i] = "rgba(255, 99, 132, 0.8)";
                    }
                    else
                    {
                        workingHoursData.Colors[i] = "rgba(54, 162, 235, 0.8)";
                    }
                    absenteeismData.Labels[i] = absenteeism[i].Username;
                    absenteeismData.Values[i] = absenteeism[i].Value;
                    if (absenteeism[i].Current == true)
                    {
                        absenteeismData.Colors[i] = "rgba(255, 99, 132, 0.8)";
                    }
                    else
                    {
                        absenteeismData.Colors[i] = "rgba(54, 162, 235, 0.8)";
                    }
                    rankData.Labels[i] = rank[i].Username;
                    rankData.Values[i] = rank[i].Value;
                    if (rank[i].Current == true)
                    {
                        rankData.Colors[i] = "rgba(255, 99, 132, 0.8)";
                    }
                    else
                    {
                        rankData.Colors[i] = "rgba(54, 162, 235, 0.8)";
                    }
                }


                model.Years.Add(new YearData
                {
                    ID = year.YearsId,
                    Year = year.YearValue.ToString(),
                    TotalAbsenteeism = year.TotalAbsenteeism.Value,
                    TotalWorkingHours = Math.Round(year.TotalWorkingHours.Value, 2),
                    TotalOvertime = Math.Round(year.TotalOvertime.Value, 2),
                    EmployeeEfficiency = Math.Round(year.EmployeeEfficiency.Value, 2),
                    Rank = year.Rank.Value,
                    YearOvertime = overtimeData,
                    YearEfficiency = efficiencyData,
                    YearWorkingHours = workingHoursData,
                    YearAbsenteeism = absenteeismData,
                    YearRank = rankData
                });
            }
            #endregion


            return PartialView("_YearsSectionPartial", model);
        }

        /// <summary>
        /// Seçilen yýlýn aylarýný döndüren action
        /// </summary>
        /// <param name="id">Yýl id</param>
        /// <returns>_MonthsSectionPartial.cshtml + MonthsSectionPartialViewModel</returns>
        public PartialViewResult YearSelect(int id)
        {
            MonthsSectionPartialViewModel model = new MonthsSectionPartialViewModel();
            model.Months = new List<MonthData>();
            #region Seçili yýlýn aylarýnýn istatistiklerinin modele eklenmesi
            var year = db.Years.Find(id);
            var years = db.Years.Where(x => x.YearValue == year.YearValue).ToList();
            foreach (var item in years)
            {
                item.Months = db.Months.Where(x => x.YearsId == item.YearsId).ToList();
            }
            foreach (var month in years.FirstOrDefault(x => x.UserStatisticId == year.UserStatisticId).Months.ToList())
            {
                //aylarýn istatistilerinin eklenmesi
                List<MonthGraphModel> overtimes = new List<MonthGraphModel>();
                List<MonthGraphModel> efficiency = new List<MonthGraphModel>();
                List<MonthGraphModel> workingHours = new List<MonthGraphModel>();
                List<MonthGraphModel> absenteeism = new List<MonthGraphModel>();
                List<MonthGraphModel> rank = new List<MonthGraphModel>();

                foreach (var y in years)
                {
                    foreach (var m in y.Months.Where(x => x.MonthValue == month.MonthValue))
                    {
                        overtimes.Add(new MonthGraphModel
                        {
                            Value = Math.Round(m.TotalOvertime.Value, 2),
                            Username = db.Users.Find(y.UserStatisticId).Username,
                            Current = y.UserStatisticId == year.UserStatisticId ? true : false
                        });
                        efficiency.Add(new MonthGraphModel
                        {
                            Value = Math.Round(m.EmployeeEfficiency.Value, 2),
                            Username = db.Users.Find(y.UserStatisticId).Username,
                            Current = y.UserStatisticId == year.UserStatisticId ? true : false
                        });
                        workingHours.Add(new MonthGraphModel
                        {
                            Value = Math.Round(m.TotalWorkingHours.Value, 2),
                            Username = db.Users.Find(y.UserStatisticId).Username,
                            Current = y.UserStatisticId == year.UserStatisticId ? true : false
                        });
                        absenteeism.Add(new MonthGraphModel
                        {
                            Value = m.TotalAbsenteeism.Value,
                            Username = db.Users.Find(y.UserStatisticId).Username,
                            Current = y.UserStatisticId == year.UserStatisticId ? true : false
                        });
                        rank.Add(new MonthGraphModel
                        {
                            Value = m.Rank.Value,
                            Username = db.Users.Find(y.UserStatisticId).Username,
                            Current = y.UserStatisticId == year.UserStatisticId ? true : false
                        });
                    }
                }

                //aylarýn istatistilerinin grafik verilerinin eklenmesi
                MonthGraphData overtimeData = new MonthGraphData();
                overtimeData.Labels = new string[overtimes.Count];
                overtimeData.Values = new double[overtimes.Count];
                overtimeData.Colors = new string[overtimes.Count];
                MonthGraphData efficiencyData = new MonthGraphData();
                efficiencyData.Labels = new string[efficiency.Count];
                efficiencyData.Values = new double[efficiency.Count];
                efficiencyData.Colors = new string[efficiency.Count];
                MonthGraphData workingHoursData = new MonthGraphData();
                workingHoursData.Labels = new string[workingHours.Count];
                workingHoursData.Values = new double[workingHours.Count];
                workingHoursData.Colors = new string[workingHours.Count];
                MonthGraphData absenteeismData = new MonthGraphData();
                absenteeismData.Labels = new string[absenteeism.Count];
                absenteeismData.Values = new double[absenteeism.Count];
                absenteeismData.Colors = new string[absenteeism.Count];
                MonthGraphData rankData = new MonthGraphData();
                rankData.Labels = new string[rank.Count];
                rankData.Values = new double[rank.Count];
                rankData.Colors = new string[rank.Count];

                for (int i = 0; i < overtimes.Count; i++)
                {
                    overtimeData.Labels[i] = overtimes[i].Username;
                    overtimeData.Values[i] = overtimes[i].Value;
                    if (overtimes[i].Current == true)
                    {
                        overtimeData.Colors[i] = "rgba(255, 99, 132, 0.8)";
                    }
                    else
                    {
                        overtimeData.Colors[i] = "rgba(54, 162, 235, 0.8)";
                    }
                    efficiencyData.Labels[i] = efficiency[i].Username;
                    efficiencyData.Values[i] = efficiency[i].Value;
                    if (efficiency[i].Current == true)
                    {
                        efficiencyData.Colors[i] = "rgba(255, 99, 132, 0.8)";
                    }
                    else
                    {
                        efficiencyData.Colors[i] = "rgba(54, 162, 235, 0.8)";
                    }
                    workingHoursData.Labels[i] = workingHours[i].Username;
                    workingHoursData.Values[i] = workingHours[i].Value;
                    if (workingHours[i].Current == true)
                    {
                        workingHoursData.Colors[i] = "rgba(255, 99, 132, 0.8)";
                    }
                    else
                    {
                        workingHoursData.Colors[i] = "rgba(54, 162, 235, 0.8)";
                    }
                    absenteeismData.Labels[i] = absenteeism[i].Username;
                    absenteeismData.Values[i] = absenteeism[i].Value;
                    if (absenteeism[i].Current == true)
                    {
                        absenteeismData.Colors[i] = "rgba(255, 99, 132, 0.8)";
                    }
                    else
                    {
                        absenteeismData.Colors[i] = "rgba(54, 162, 235, 0.8)";
                    }
                    rankData.Labels[i] = rank[i].Username;
                    rankData.Values[i] = rank[i].Value;
                    if (rank[i].Current == true)
                    {
                        rankData.Colors[i] = "rgba(255, 99, 132, 0.8)";
                    }
                    else
                    {
                        rankData.Colors[i] = "rgba(54, 162, 235, 0.8)";
                    }
                }

                model.Months.Add(new MonthData
                {
                    ID = month.MonthId,
                    Month = month.MonthValue.ToString(),
                    TotalAbsenteeism = month.TotalAbsenteeism.Value,
                    TotalWorkingHours = Math.Round(month.TotalWorkingHours.Value, 2),
                    TotalOvertime = Math.Round(month.TotalOvertime.Value, 2),
                    EmployeeEfficiency = Math.Round(month.EmployeeEfficiency.Value, 2),
                    Rank = month.Rank.Value,
                    MonthOvertime = overtimeData,
                    MonthEfficiency = efficiencyData,
                    MonthWorkingHours = workingHoursData,
                    MonthAbsenteeism = absenteeismData,
                    MonthRank = rankData
                });
            }
            #endregion


            return PartialView("_MonthsSectionPartial", model);
        }

        /// <summary>
        /// Seçilen ayýn haftalarýný döndüren action
        /// </summary>
        /// <param name="id">Ay id</param>
        /// <returns>_WeeksSectionPartial.cshtml + WeekSectionPartialViewModel</returns>
        public PartialViewResult MonthSelect(int id)
        {
            WeekSectionPartialViewModel model = new WeekSectionPartialViewModel();
            model.Weeks = new List<WeekData>();
            #region Seçili ayýn haftalarýnýn istatistiklerinin model eklenmesi
            var month = db.Months.Find(id);
            month.Years = db.Years.Find(month.YearsId);
            var years = db.Years.Where(x => x.YearValue == month.Years.YearValue).ToList();
            foreach (var year in years.ToList())
            {
                year.Months = db.Months.Where(x => x.YearsId == year.YearsId).ToList();
                foreach (var itmMonth in year.Months.Where(x => x.MonthValue == month.MonthValue).ToList())
                {
                    itmMonth.Weeks = db.Weeks.Where(x => x.MonthId == itmMonth.MonthId).ToList();
                }
            }

            foreach (var week in years.FirstOrDefault(x => x.UserStatisticId == month.Years.UserStatisticId).Months.ToList().FirstOrDefault(x => x.MonthValue == month.MonthValue).Weeks.ToList())
            {
                //haftalarýn istatistilerinin eklenmesi
                List<WeekGraphModel> overtimes = new List<WeekGraphModel>();
                List<WeekGraphModel> efficiency = new List<WeekGraphModel>();
                List<WeekGraphModel> workingHours = new List<WeekGraphModel>();
                List<WeekGraphModel> absenteeism = new List<WeekGraphModel>();
                List<WeekGraphModel> rank = new List<WeekGraphModel>();
                foreach (var y in years)
                {
                    foreach (var m in y.Months.Where(x => x.MonthValue == month.MonthValue))
                    {
                        foreach (var w in m.Weeks.Where(x => x.WeekValue == week.WeekValue))
                        {
                            overtimes.Add(new WeekGraphModel
                            {
                                Value = Math.Round(w.TotalOvertime.Value, 2),
                                Username = db.Users.Find(y.UserStatisticId).Username,
                                Current = y.UserStatisticId == month.Years.UserStatisticId ? true : false
                            });
                            efficiency.Add(new WeekGraphModel
                            {
                                Value = Math.Round(w.EmployeeEfficiency.Value, 2),
                                Username = db.Users.Find(y.UserStatisticId).Username,
                                Current = y.UserStatisticId == month.Years.UserStatisticId ? true : false
                            });
                            workingHours.Add(new WeekGraphModel
                            {
                                Value = Math.Round(w.TotalWorkingHours.Value, 2),
                                Username = db.Users.Find(y.UserStatisticId).Username,
                                Current = y.UserStatisticId == month.Years.UserStatisticId ? true : false
                            });
                            absenteeism.Add(new WeekGraphModel
                            {
                                Value = w.TotalAbsenteeism.Value,
                                Username = db.Users.Find(y.UserStatisticId).Username,
                                Current = y.UserStatisticId == month.Years.UserStatisticId ? true : false
                            });
                            rank.Add(new WeekGraphModel
                            {
                                Value = w.Rank.Value,
                                Username = db.Users.Find(y.UserStatisticId).Username,
                                Current = y.UserStatisticId == month.Years.UserStatisticId ? true : false
                            });
                        }
                    }
                }

                //haftalarýn istatistilerinin grafik verilerinin eklenmesi
                WeekGraphData overtimeData = new WeekGraphData();
                overtimeData.Labels = new string[overtimes.Count];
                overtimeData.Values = new double[overtimes.Count];
                overtimeData.Colors = new string[overtimes.Count];
                WeekGraphData efficiencyData = new WeekGraphData();
                efficiencyData.Labels = new string[efficiency.Count];
                efficiencyData.Values = new double[efficiency.Count];
                efficiencyData.Colors = new string[efficiency.Count];
                WeekGraphData workingHoursData = new WeekGraphData();
                workingHoursData.Labels = new string[workingHours.Count];
                workingHoursData.Values = new double[workingHours.Count];
                workingHoursData.Colors = new string[workingHours.Count];
                WeekGraphData absenteeismData = new WeekGraphData();
                absenteeismData.Labels = new string[absenteeism.Count];
                absenteeismData.Values = new double[absenteeism.Count];
                absenteeismData.Colors = new string[absenteeism.Count];
                WeekGraphData rankData = new WeekGraphData();
                rankData.Labels = new string[rank.Count];
                rankData.Values = new double[rank.Count];
                rankData.Colors = new string[rank.Count];

                for (int i = 0; i < overtimes.Count; i++)
                {
                    overtimeData.Labels[i] = overtimes[i].Username;
                    overtimeData.Values[i] = overtimes[i].Value;
                    if (overtimes[i].Current == true)
                    {
                        overtimeData.Colors[i] = "rgba(255, 99, 132, 0.8)";
                    }
                    else
                    {
                        overtimeData.Colors[i] = "rgba(54, 162, 235, 0.8)";
                    }
                    efficiencyData.Labels[i] = efficiency[i].Username;
                    efficiencyData.Values[i] = efficiency[i].Value;
                    if (efficiency[i].Current == true)
                    {
                        efficiencyData.Colors[i] = "rgba(255, 99, 132, 0.8)";
                    }
                    else
                    {
                        efficiencyData.Colors[i] = "rgba(54, 162, 235, 0.8)";
                    }
                    workingHoursData.Labels[i] = workingHours[i].Username;
                    workingHoursData.Values[i] = workingHours[i].Value;
                    if (workingHours[i].Current == true)
                    {
                        workingHoursData.Colors[i] = "rgba(255, 99, 132, 0.8)";
                    }
                    else
                    {
                        workingHoursData.Colors[i] = "rgba(54, 162, 235, 0.8)";
                    }
                    absenteeismData.Labels[i] = absenteeism[i].Username;
                    absenteeismData.Values[i] = absenteeism[i].Value;
                    if (absenteeism[i].Current == true)
                    {
                        absenteeismData.Colors[i] = "rgba(255, 99, 132, 0.8)";
                    }
                    else
                    {
                        absenteeismData.Colors[i] = "rgba(54, 162, 235, 0.8)";
                    }
                    rankData.Labels[i] = rank[i].Username;
                    rankData.Values[i] = rank[i].Value;
                    if (rank[i].Current == true)
                    {
                        rankData.Colors[i] = "rgba(255, 99, 132, 0.8)";
                    }
                    else
                    {
                        rankData.Colors[i] = "rgba(54, 162, 235, 0.8)";
                    }
                }

                model.Weeks.Add(new WeekData
                {
                    ID = week.WeekId,
                    Week = week.WeekValue.ToString(),
                    TotalAbsenteeism = week.TotalAbsenteeism.Value,
                    TotalWorkingHours = Math.Round(week.TotalWorkingHours.Value, 2),
                    TotalOvertime = Math.Round(week.TotalOvertime.Value, 2),
                    EmployeeEfficiency = Math.Round(week.EmployeeEfficiency.Value, 2),
                    Rank = week.Rank.Value,
                    WeekOvertime = overtimeData,
                    WeekEfficiency = efficiencyData,
                    WeekWorkingHours = workingHoursData,
                    WeekAbsenteeism = absenteeismData,
                    WeekRank = rankData
                });
            }

            #endregion

            return PartialView("_WeeksSectionPartial", model);
        }

        /// <summary>
        /// Seçilen haftanýn günlerini döndüren action
        /// </summary>
        /// <param name="id">Hafta id</param>
        /// <returns>_DaysSectionPartial.cshtml + DaySectionViewModel</returns>
        public PartialViewResult WeekSelect(int id)
        {
            DaySectionViewModel model = new DaySectionViewModel();
            model.Days = new List<DayData>();

            #region Seçili haftanýn günlerinin istatistiklerinin modele eklenmesi
            var week = db.Weeks.Find(id);
            var month = db.Months.Find(week.MonthId);
            month.Years = db.Years.Find(month.YearsId);

            var years = db.Years.Where(x => x.YearValue == month.Years.YearValue).ToList();
            foreach (var year in years.ToList())
            {
                year.Months = db.Months.Where(x => x.YearsId == year.YearsId).ToList();
                foreach (var itmMonth in year.Months.Where(x => x.MonthValue == month.MonthValue).ToList())
                {
                    itmMonth.Weeks = db.Weeks.Where(x => x.MonthId == itmMonth.MonthId).ToList();
                    foreach (var itmWeek in itmMonth.Weeks.Where(x => x.WeekValue == week.WeekValue))
                    {
                        itmWeek.Days = db.Days.Where(x => x.WeekId == itmWeek.WeekId).ToList();
                    }
                }
            }

            foreach (var day in years.FirstOrDefault(x => x.UserStatisticId == month.Years.UserStatisticId).Months.ToList().FirstOrDefault(x => x.MonthValue == month.MonthValue).Weeks.ToList().FirstOrDefault(x => x.WeekValue == week.WeekValue).Days.ToList())
            {
                //günlerin istatistilerinin eklenmesi
                List<DayGraphModel> overtimes = new List<DayGraphModel>();
                List<DayGraphModel> efficiency = new List<DayGraphModel>();
                List<DayGraphModel> workingHours = new List<DayGraphModel>();
                List<DayGraphModel> absenteeism = new List<DayGraphModel>();
                List<DayGraphModel> rank = new List<DayGraphModel>();
                foreach (var y in years)
                {
                    foreach (var m in y.Months.Where(x => x.MonthValue == month.MonthValue))
                    {
                        foreach (var w in m.Weeks.Where(x => x.WeekValue == week.WeekValue))
                        {
                            foreach (var d in w.Days.Where(x => x.DayValue == day.DayValue))
                            {
                                overtimes.Add(new DayGraphModel
                                {
                                    Value = d.Overtime != null ? Math.Round(d.Overtime.Value, 2) : 0,
                                    Username = db.Users.Find(y.UserStatisticId).Username,
                                    Current = y.UserStatisticId == month.Years.UserStatisticId ? true : false
                                });
                                efficiency.Add(new DayGraphModel
                                {
                                    Value = d.EmployeeEfficiency != null ? Math.Round(d.EmployeeEfficiency.Value, 2) : 0,
                                    Username = db.Users.Find(y.UserStatisticId).Username,
                                    Current = y.UserStatisticId == month.Years.UserStatisticId ? true : false
                                });
                                workingHours.Add(new DayGraphModel
                                {
                                    Value = d.WorkingHours != null ? Math.Round(d.WorkingHours.Value, 2) : 0,
                                    Username = db.Users.Find(y.UserStatisticId).Username,
                                    Current = y.UserStatisticId == month.Years.UserStatisticId ? true : false
                                });
                                absenteeism.Add(new DayGraphModel
                                {
                                    Value = d.IsAbsenteeism != null ? d.IsAbsenteeism == true ? 1 : 0 : 0,
                                    Username = db.Users.Find(y.UserStatisticId).Username,
                                    Current = y.UserStatisticId == month.Years.UserStatisticId ? true : false
                                });
                                rank.Add(new DayGraphModel
                                {
                                    Value = d.Rank != null ? d.Rank.Value : 0,
                                    Username = db.Users.Find(y.UserStatisticId).Username,
                                    Current = y.UserStatisticId == month.Years.UserStatisticId ? true : false
                                });
                            }
                        }
                    }
                }

                //günlerin istatistilerinin grafik verilerinin eklenmesi
                DayGraphData overtimeData = new DayGraphData();
                overtimeData.Labels = new string[overtimes.Count];
                overtimeData.Values = new double[overtimes.Count];
                overtimeData.Colors = new string[overtimes.Count];
                DayGraphData efficiencyData = new DayGraphData();
                efficiencyData.Labels = new string[efficiency.Count];
                efficiencyData.Values = new double[efficiency.Count];
                efficiencyData.Colors = new string[efficiency.Count];
                DayGraphData workingHoursData = new DayGraphData();
                workingHoursData.Labels = new string[workingHours.Count];
                workingHoursData.Values = new double[workingHours.Count];
                workingHoursData.Colors = new string[workingHours.Count];
                DayGraphData absenteeismData = new DayGraphData();
                absenteeismData.Labels = new string[absenteeism.Count];
                absenteeismData.Values = new double[absenteeism.Count];
                absenteeismData.Colors = new string[absenteeism.Count];
                DayGraphData rankData = new DayGraphData();
                rankData.Labels = new string[rank.Count];
                rankData.Values = new double[rank.Count];
                rankData.Colors = new string[rank.Count];

                for (int i = 0; i < overtimes.Count; i++)
                {
                    overtimeData.Labels[i] = overtimes[i].Username;
                    overtimeData.Values[i] = overtimes[i].Value;
                    if (overtimes[i].Current == true)
                    {
                        overtimeData.Colors[i] = "rgba(255, 99, 132, 0.8)";
                    }
                    else
                    {
                        overtimeData.Colors[i] = "rgba(54, 162, 235, 0.8)";
                    }
                    efficiencyData.Labels[i] = efficiency[i].Username;
                    efficiencyData.Values[i] = efficiency[i].Value;
                    if (efficiency[i].Current == true)
                    {
                        efficiencyData.Colors[i] = "rgba(255, 99, 132, 0.8)";
                    }
                    else
                    {
                        efficiencyData.Colors[i] = "rgba(54, 162, 235, 0.8)";
                    }
                    workingHoursData.Labels[i] = workingHours[i].Username;
                    workingHoursData.Values[i] = workingHours[i].Value;
                    if (workingHours[i].Current == true)
                    {
                        workingHoursData.Colors[i] = "rgba(255, 99, 132, 0.8)";
                    }
                    else
                    {
                        workingHoursData.Colors[i] = "rgba(54, 162, 235, 0.8)";
                    }
                    absenteeismData.Labels[i] = absenteeism[i].Username;
                    absenteeismData.Values[i] = absenteeism[i].Value;
                    if (absenteeism[i].Current == true)
                    {
                        absenteeismData.Colors[i] = "rgba(255, 99, 132, 0.8)";
                    }
                    else
                    {
                        absenteeismData.Colors[i] = "rgba(54, 162, 235, 0.8)";
                    }
                    rankData.Labels[i] = rank[i].Username;
                    rankData.Values[i] = rank[i].Value;
                    if (rank[i].Current == true)
                    {
                        rankData.Colors[i] = "rgba(255, 99, 132, 0.8)";
                    }
                    else
                    {
                        rankData.Colors[i] = "rgba(54, 162, 235, 0.8)";
                    }
                }

                model.Days.Add(new DayData
                {
                    ID = day.DayId,
                    Day = day.DayValue.ToString(),
                    Absenteeism = day.IsAbsenteeism != null ? day.IsAbsenteeism == true ? (byte)1 : (byte)0 : (byte)0,
                    WorkingHours = day.WorkingHours != null ? Math.Round(day.WorkingHours.Value, 2) : 0,
                    Overtime = day.Overtime != null ? Math.Round(day.Overtime.Value, 2) : 0,
                    Efficiency = day.EmployeeEfficiency != null ? Math.Round(day.EmployeeEfficiency.Value, 2) : 0,
                    Rank = day.Rank != null ? day.Rank.Value : (byte)0,
                    DayOvertime = overtimeData,
                    DayEfficiency = efficiencyData,
                    DayWorkingHours = workingHoursData,
                    DayAbsenteeism = absenteeismData,
                    DayRank = rankData,
                    Entry = day.EntryTime,
                    Exit = day.ExitTime
                });
            }

            #endregion

            return PartialView("_DaysSectionPartial", model);
        }

        /// <summary>
        /// General Statistics sayfasýný açan action
        /// </summary>
        /// <returns>GeneralStatistics.cshtml</returns>
        public IActionResult GeneralStatistics()
        {
            return View();
        }

        /// <summary>
        /// Tüm çalýþanlarýn listesini döndüren action
        /// </summary>
        /// <returns>Json ile serialize edilmiþ SelectList türünde çalýþan listesi</returns>
        public IActionResult GetEmployees()
        {
            List<SelectListItem> employees = new List<SelectListItem>();
            var employeesList = db.Users.ToList();
            IEnumerable<User> query = employeesList.ToList();
            foreach (var employee in query)
            {
                employees.Add(new SelectListItem
                {
                    Text = employee.Username,
                    Value = employee.UserId.ToString()
                });
            }
            var data = new SelectList(employees, "Value", "Text");
            return Json(data);
        }

        /// <summary>
        /// seçili çalýþanýn yýl listesini döndüren action
        /// </summary>
        /// <param name="id">Çalýþan id</param>
        /// <returns>Json ile serialize edilmiþ SelectList türünde yýl listesi</returns>
        public IActionResult GetYears(string id)
        {
            List<SelectListItem> years = new List<SelectListItem>();
            var yearList = db.Years.Where(x => x.UserStatisticId == Convert.ToInt32(id)).ToList();
            IEnumerable<Year> query = yearList.ToList();
            foreach (var year in query)
            {
                years.Add(new SelectListItem
                {
                    Text = year.YearValue.ToString(),
                    Value = year.YearsId.ToString()
                });
            }
            var data = new SelectList(years, "Value", "Text");
            return Json(data);
        }

        /// <summary>
        /// Seçili yýlýn aylarýný döndüren action
        /// </summary>
        /// <param name="id">Ay id</param>
        /// <returns>Json ile serialize edilmiþ SelectList türünde ay listesi</returns>
        public IActionResult GetMonths(string id)
        {
            List<SelectListItem> months = new List<SelectListItem>();
            var monthList = db.Months.Where(x => x.YearsId == Convert.ToInt32(id)).ToList();
            IEnumerable<Month> query = monthList.ToList();
            foreach (var month in query)
            {
                months.Add(new SelectListItem
                {
                    Text = month.MonthValue.ToString(),
                    Value = month.MonthId.ToString()
                });
            }
            var data = new SelectList(months, "Value", "Text");
            return Json(data);
        }

        //public IActionResult GetDays(string id)
        //{
        //    List<SelectListItem> days = new List<SelectListItem>();
        //    var weekList = db.Weeks.Where(x => x.MonthId == Convert.ToInt32(id)).ToList();
        //    List<Day> dayList = new List<Day>();
        //    foreach (var week in weekList)
        //    {
        //        week.Days = db.Days.Where(x => x.WeekId == week.WeekId).ToList();
        //        foreach (var day in week.Days.ToList())
        //        {
        //            dayList.Add(day);
        //        }
        //    }
        //    IEnumerable<Day> query = dayList.ToList();

        //    foreach (var day in query)
        //    {
        //        days.Add(new SelectListItem
        //        {
        //            Text = day.DayValue.ToString(),
        //            Value = day.DayId.ToString()
        //        });
        //    }
        //    var data = new SelectList(days, "Value", "Text");
        //    return Json(data);
        //}

        /// <summary>
        /// Seçili filtrelere göre çalýþanýn istatistiklerini döndüren action
        /// </summary>
        /// <param name="emp">Çalýþan id</param>
        /// <param name="year">Yýl id</param>
        /// <param name="month">Ay id</param>
        /// <param name="ft">Ýstatistik türü</param>
        /// <returns>_GeneralStatisticsCanvasPartial.cshtml + GeneralStatisticsPartialViewModel</returns>
        public PartialViewResult ApplyFilter(int emp, int year, int month, int ft)
        {
            GeneralStatisticsPartialViewModel model = new GeneralStatisticsPartialViewModel();
            //filtre istek id'sinin eklenmesi
            model.ID = FilterRequestRepo.Count;
            FilterRequestRepo.IncrementCount(); //filtre isteklerinin sayýsýnýn arttýrýlmasý

            List<string> labels = new List<string>();
            List<double> values = new List<double>();
            //eðer filterede ay seçilmiþse
            if (month > 0)
            {
                //ay istatistiklerinin eklenmesi
                var _month = db.Months.Find(month);
                _month.Weeks = db.Weeks.Where(x => x.MonthId == month).ToList();
                model.Efficiency = Math.Round(_month.EmployeeEfficiency.Value, 2);
                model.Overtime = Math.Round(_month.TotalOvertime.Value, 2);
                model.WorkingHours = Math.Round(_month.TotalWorkingHours.Value, 2);
                model.Absenteeism = _month.TotalAbsenteeism.Value;
                model.Rank = _month.Rank.Value;
                model.Val = _month.MonthValue.ToString();
                foreach (var week in _month.Weeks.ToList())
                {
                    week.Days = db.Days.Where(x => x.WeekId == week.WeekId).ToList();
                    foreach (var day in week.Days.ToList())
                    {
                        labels.Add(day.DayValue.ToString());
                        double val = 0;
                        switch (ft)
                        {
                            case 1:
                                val = day.WorkingHours != null ? Math.Round(day.WorkingHours.Value, 2) : 0;
                                values.Add(val);
                                break;
                            case 2:
                                val = day.Overtime != null ? Math.Round(day.Overtime.Value, 2) : 0;
                                values.Add(val);
                                break;
                            case 3:
                                val = day.IsAbsenteeism != null ? day.IsAbsenteeism == true ? 1 : 0 : 0;
                                values.Add(val);
                                break;
                            case 4:
                                val = day.EmployeeEfficiency != null ? Math.Round(day.EmployeeEfficiency.Value, 2) : 0;
                                values.Add(val);
                                break;
                            case 5:
                                val = day.Rank != null ? day.Rank.Value : 0;
                                values.Add(val);
                                break;
                        }
                    }
                }
                model.Labels = labels.ToArray();
                model.Values = values.ToArray();
            }
            else
            {
                //yýl istatistiklerinin eklenmesi
                var _year = db.Years.Find(year);
                _year.Months = db.Months.Where(x => x.YearsId == year).ToList();
                model.Efficiency = Math.Round(_year.EmployeeEfficiency.Value, 2);
                model.Overtime = Math.Round(_year.TotalOvertime.Value, 2);
                model.WorkingHours = Math.Round(_year.TotalWorkingHours.Value, 2);
                model.Absenteeism = _year.TotalAbsenteeism.Value;
                model.Rank = _year.Rank.Value;
                model.Val = _year.YearValue.ToString();
                foreach (var item in _year.Months)
                {
                    labels.Add(item.MonthValue.ToString());
                    switch (ft)
                    {
                        case 1:
                            values.Add(Math.Round(item.TotalWorkingHours.Value, 2));
                            break;
                        case 2:
                            values.Add(Math.Round(item.TotalOvertime.Value, 2));
                            break;
                        case 3:
                            values.Add(item.TotalAbsenteeism.Value);
                            break;
                        case 4:
                            values.Add(Math.Round(item.EmployeeEfficiency.Value, 2));
                            break;
                        case 5:
                            values.Add(item.Rank.Value);
                            break;
                    }
                }
                model.Labels = labels.ToArray();
                model.Values = values.ToArray();
            }

            //istatistik türüne göre grafik baþlýðýnýn belirlenmesi
            switch (ft)
            {
                case 1:
                    model.Title = "Working Hours";
                    break;
                case 2:
                    model.Title = "Overtime";
                    break;
                case 3:
                    model.Title = "Absenteeism";
                    break;
                case 4:
                    model.Title = "Efficiency";
                    break;
                case 5:
                    model.Title = "Rank";
                    break;
            }

            return PartialView("_GeneralStatisticsCanvasPartial", model);
        }
    }
}
