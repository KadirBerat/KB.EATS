using KB.EATS.WebApi.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KB.EATS.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserShiftManager : ControllerBase
    {
        private KbEatsContext db = new KbEatsContext();

        private readonly ILogger<UserShiftManager> _logger;

        public UserShiftManager(ILogger<UserShiftManager> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "AddShiftData")]
        public IActionResult AddShiftData(ShiftData shiftData, int userId)
        {
            int result = 0;


            UserShift shift = db.UserShifts.Where(x => x.UserId == userId && x.Date.Value.ToString("dd-MM-yyyy") == shiftData.FullDate.Value.ToString("dd-MM-yyyy")).FirstOrDefault();

            if (shift != null)
            {
                db.UserShifts.Find(shift.UserShiftId).ShiftData.Add(new ShiftData { Date = shiftData.Date, FullDate = shiftData.FullDate, Time = shiftData.Time });
                result = db.SaveChanges();

                //mesai verisi eklendi analiz için veritabanında ilgili tarih varmı diye kontrol ediyorum ve yoksa ekliyorum
                /*
                 Bir günün mesai çıkış kaydı eklendiği için o günün analizini yapacağım fakat bu gün haftanın ilk mesai günü ise bir önceki haftanın analizini de yapacağım.
                 Ve yine bu gün ayın ilk mesai günü ise bir önceki ayın analizini yapacağım, ve yine eğer bu gün yılın ilk mesai günü ise bir önceki yılın analizini yapacağım.
                 Bu sayede Kullanıcının günlük, haftalık, aylık ve yıllık istatistikleri hesaplanmış olacak.
                 */
                Year year = db.Users.Find(userId).UserStatistic.Years.FirstOrDefault(x => x.YearValue.ToString() == shift.Date.Value.ToString("yyyy"));
                if (year != null)
                {
                    Month month = year.Months.FirstOrDefault(x => x.MonthValue.ToString() == shift.Date.Value.ToString("MM"));
                    if (month != null)
                    {
                        Week week = month.Weeks.FirstOrDefault(x => x.WeekValue.ToString() == GetWeekNumberOfMonth(shift.Date.Value).ToString());
                        if (week != null)
                        {
                            Day day = week.Days.FirstOrDefault(x => x.DayValue.ToString() == shift.Date.Value.ToString("dd"));
                            if (day == null)
                            {
                                week.Days.Add(new Day()
                                {
                                    DayValue = Convert.ToByte(shift.Date.Value.ToString("dd"))
                                });
                            }
                        }
                        else
                        {
                            month.Weeks.Add(new Week()
                            {
                                WeekValue = Convert.ToByte(GetWeekNumberOfMonth(shift.Date.Value)),
                                Days = new List<Day>()
                                {
                                    new Day()
                                    {
                                        DayValue = Convert.ToByte(shift.Date.Value.ToString("dd"))
                                    }
                                }
                            });
                        }
                    }
                    else
                    {
                        year.Months.Add(new Month()
                        {
                            MonthValue = Convert.ToByte(shift.Date.Value.ToString("MM")),
                            Weeks = new List<Week>()
                            {
                                new Week()
                                {
                                    WeekValue = Convert.ToByte(GetWeekNumberOfMonth(shift.Date.Value)),
                                    Days = new List<Day>()
                                    {
                                        new Day()
                                        {
                                            DayValue = Convert.ToByte(shift.Date.Value.ToString("dd"))
                                        }
                                    }
                                }
                            }
                        });
                    }
                }
                else
                {
                    db.Users.Find(userId).UserStatistic.Years.Add(new Year
                    {
                        YearValue = Convert.ToInt16(shift.Date.Value.ToString("yyyy")),
                        Months = new List<Month>()
                        {
                            new Month()
                            {
                                 MonthValue = Convert.ToByte(shift.Date.Value.ToString("MM")),
                                  Weeks = new List<Week>()
                                  {
                                      new Week()
                                      {
                                          WeekValue = Convert.ToByte(GetWeekNumberOfMonth(shift.Date.Value)),
                                           Days = new List<Day>()
                                           {
                                               new Day()
                                               {
                                                    DayValue = Convert.ToByte(shift.Date.Value.ToString("dd"))
                                               }
                                           }

                                      }
                                  }
                            }
                        }
                    });
                }

                result = db.SaveChanges();

                //analiz
            }
            else
            {
                db.Users.Find(userId).UserShifts.Add(new UserShift
                {
                    Date = shiftData.Date,
                    ShiftData = new List<ShiftData>()
                        {
                            new ShiftData
                            {
                                Date = shiftData.Date,
                                FullDate = shiftData.FullDate,
                                Time = shiftData.Time
                            }
                        }
                });
                result = db.SaveChanges();
            }


            if (result == 0)
            {
                return BadRequest("An error occurred");
            }
            else
            {
                return Ok("Successful");
            }
        }

        private int GetWeekNumberOfMonth(DateTime date)
        {
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            DayOfWeek firstDayOfWeek = DayOfWeek.Monday;
            int weekNumber = (date.Day + GetDayOfWeekOffset(firstDayOfMonth, firstDayOfWeek) - 1) / 7 + 1;
            return weekNumber;
        }

        private int GetDayOfWeekOffset(DateTime date, DayOfWeek firstDayOfWeek)
        {
            int offset = date.DayOfWeek - firstDayOfWeek;
            if (offset < 0)
            {
                offset += 7;
            }
            return offset;
        }
    }
}
