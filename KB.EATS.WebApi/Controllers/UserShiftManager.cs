using KB.EATS.WebApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KB.EATS.WebApi.Controllers
{
    [ApiController]
    [Route("api/shift")]
    public class UserShiftManager : ControllerBase
    {
        private KbEatsContext db = new KbEatsContext(); //veritabanı bağlantısı için kullanıyorum

        private readonly ILogger<UserShiftManager> _logger; //loglama için kullanıyorum

        //loglama için kullanıyorum
        public UserShiftManager(ILogger<UserShiftManager> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Fiziksel ortamdan gelen mesai giriş veya çıkış verisini veri tabanıma kaydediyorum ve eğer mesai çıkış verisi ise analizini yapıyorum.
        /// Ayrıca analiz sonucunda kullanıcının günlük, haftalık, aylık ve yıllık istatistiklerini hesaplıyorum.
        /// </summary>
        /// <param name="shiftData">mesai verisi</param>
        /// <param name="userId">çalışan id</param>
        /// <returns></returns>
        [HttpPost("add")] //http post methodu ve add isimli bir endpoint oluşturdum
        public IActionResult AddShiftData(ShiftData shiftData, int userId)
        {
            int result = 0;

            //kullanıcının mesai verisini alıyorum
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
                                    DayValue = Convert.ToByte(shift.Date.Value.ToString("dd")),
                                    IsCalculated = false,
                                });
                            }
                        }
                        else
                        {
                            month.Weeks.Add(new Week()
                            {
                                WeekValue = Convert.ToByte(GetWeekNumberOfMonth(shift.Date.Value)),
                                IsCalculated = false,
                                Days = new List<Day>()
                                {
                                    new Day()
                                    {
                                        DayValue = Convert.ToByte(shift.Date.Value.ToString("dd")),
                                        IsCalculated = false,
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
                            IsCalculated = false,
                            Weeks = new List<Week>()
                            {
                                new Week()
                                {
                                    WeekValue = Convert.ToByte(GetWeekNumberOfMonth(shift.Date.Value)),
                                    IsCalculated = false,
                                    Days = new List<Day>()
                                    {
                                        new Day()
                                        {
                                            DayValue = Convert.ToByte(shift.Date.Value.ToString("dd")),
                                            IsCalculated = false,
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
                        IsCalculated = false,
                        Months = new List<Month>()
                        {
                            new Month()
                            {
                                 MonthValue = Convert.ToByte(shift.Date.Value.ToString("MM")),
                                 IsCalculated = false,
                                  Weeks = new List<Week>()
                                  {
                                      new Week()
                                      {
                                          WeekValue = Convert.ToByte(GetWeekNumberOfMonth(shift.Date.Value)),
                                          IsCalculated = false,
                                          Days = new List<Day>()
                                          {
                                              new Day()
                                              {
                                                   DayValue = Convert.ToByte(shift.Date.Value.ToString("dd")),
                                                   IsCalculated = false,
                                              }
                                          }

                                      }
                                  }
                            }
                        }
                    });
                }

                result = db.SaveChanges();

                //Örnek veiler ile çalıştığım ve zaman yetmediği için aşağıdaki kodu tamamlamadım fakat nereye ne eklenmesi gerektiğini belirttim ve analiz öncesi gerekli tüm kontrolleri yaptım. Örnek veriler üzerinde yaptığım analiz işlemlerinin kodları Test projesinde mevcut.

                //analiz
                UserStatistic _stat = db.Users.Find(userId).UserStatistic;
                Day _day = _stat.Years.LastOrDefault().Months.LastOrDefault().Weeks.LastOrDefault().Days.LastOrDefault();
                byte weekValue = _day.Week.WeekValue.Value;
                byte monthValue = _day.Week.Month.MonthValue.Value;
                short yearValue = _day.Week.Month.Years.YearValue.Value;

                if (monthValue == 1) //ocak ayındaysak önceki yılın analiz edilip edilmediğini kontrol ediyorum
                {
                    if (_stat.Years.FirstOrDefault(x => x.YearValue == yearValue - 1) != null)
                    {
                        if (_stat.Years.FirstOrDefault(x => x.YearValue == yearValue - 1).IsCalculated == false)
                        {
                            //yıl analizi
                        }
                    }
                }

                if (monthValue == 1) //ocak ayındaysak bir önceki yılın aralık ayının analiz edilip edilmediğini kontrol ediyorum
                {
                    if (_stat.Years.FirstOrDefault(x => x.YearValue == yearValue - 1) != null)
                    {
                        if (_stat.Years.FirstOrDefault(x => x.YearValue == yearValue - 1).Months.LastOrDefault() != null)
                        {
                            if (_stat.Years.FirstOrDefault(x => x.YearValue == yearValue - 1).Months.LastOrDefault().IsCalculated == false)
                            {
                                //ay analizi
                            }

                            if (_stat.Years.FirstOrDefault(x => x.YearValue == yearValue - 1).Months.LastOrDefault().Weeks.LastOrDefault() != null) //ocak ayındaysak bir önceki yılın son haftasının analiz edilip edilmediğini kontrol ediyorum
                            {
                                if (_stat.Years.FirstOrDefault(x => x.YearValue == yearValue - 1).Months.LastOrDefault().Weeks.LastOrDefault().IsCalculated == false)
                                {
                                    //hafta analizi
                                }

                                if (_stat.Years.FirstOrDefault(x => x.YearValue == yearValue - 1).Months.LastOrDefault().Weeks.LastOrDefault().Days.LastOrDefault() != null) // ocak ayındaysak bir önceki yılın son gününün analiz edilip edilmediğini kontrol ediyorum
                                {
                                    if (_stat.Years.FirstOrDefault(x => x.YearValue == yearValue - 1).Months.LastOrDefault().Weeks.LastOrDefault().Days.LastOrDefault().IsCalculated == false)
                                    {
                                        //gün analizi
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (_stat.Years.FirstOrDefault(x => x.YearValue == yearValue).Months.FirstOrDefault(x => x.MonthValue == monthValue - 1) != null) //bir önceki ayın analizi
                    {
                        if (_stat.Years.FirstOrDefault(x => x.YearValue == yearValue).Months.FirstOrDefault(x => x.MonthValue == monthValue - 1).IsCalculated == false)
                        {
                            //ay analizi
                        }

                        if (_stat.Years.FirstOrDefault().Months.FirstOrDefault(x => x.MonthValue == monthValue - 1).Weeks.LastOrDefault() != null) //bir önceki ayın son haftasının analizi
                        {
                            if (_stat.Years.FirstOrDefault(x => x.YearValue == yearValue).Months.FirstOrDefault(x => x.MonthValue == monthValue - 1).Weeks.LastOrDefault().IsCalculated == false)
                            {
                                //hafta analizi
                            }

                            if (_stat.Years.FirstOrDefault().Months.FirstOrDefault(x => x.MonthValue == monthValue - 1).Weeks.LastOrDefault().Days.LastOrDefault() != null) //bir önceki ayın son gününün analizi
                            {
                                if (_stat.Years.FirstOrDefault(x => x.YearValue == yearValue).Months.FirstOrDefault(x => x.MonthValue == monthValue - 1).Weeks.LastOrDefault().Days.LastOrDefault().IsCalculated == false)
                                {
                                    //gün analizi
                                }
                            }
                        }
                    }
                }

                if (weekValue > 1)
                {
                    if (_stat.Years.FirstOrDefault(x => x.YearValue == yearValue).Months.FirstOrDefault(x => x.MonthValue == monthValue).Weeks.FirstOrDefault(x => x.WeekValue == weekValue - 1) != null)
                    {
                        if (_stat.Years.FirstOrDefault(x => x.YearValue == yearValue).Months.FirstOrDefault(x => x.MonthValue == monthValue).Weeks.FirstOrDefault(x => x.WeekValue == weekValue - 1).IsCalculated == false)
                        {
                            //hafta analizi
                        }

                        if (_stat.Years.FirstOrDefault(x => x.YearValue == yearValue).Months.FirstOrDefault(x => x.MonthValue == monthValue).Weeks.FirstOrDefault(x => x.WeekValue == weekValue - 1).Days.LastOrDefault() != null)
                        {
                            if (_stat.Years.FirstOrDefault(x => x.YearValue == yearValue).Months.FirstOrDefault(x => x.MonthValue == monthValue).Weeks.FirstOrDefault(x => x.WeekValue == weekValue - 1).Days.LastOrDefault().IsCalculated == false)
                            {
                                //gün analizi
                            }
                        }
                    }
                }

                if (_day.IsCalculated == false)
                {
                    //gün analizi
                }
                if (_stat.Years.FirstOrDefault(x => x.YearValue == yearValue).Months.FirstOrDefault(x => x.MonthValue == monthValue).Weeks.FirstOrDefault(x => x.WeekValue == weekValue - 1).Days.FirstOrDefault(x => x.DayValue == _day.DayValue - 1) != null)
                {
                    if (_stat.Years.FirstOrDefault(x => x.YearValue == yearValue).Months.FirstOrDefault(x => x.MonthValue == monthValue).Weeks.FirstOrDefault(x => x.WeekValue == weekValue - 1).Days.FirstOrDefault(x => x.DayValue == _day.DayValue - 1).IsCalculated == false)
                    {
                        //gün analizi
                    }
                }


            }
            else //mesai giriş verisi ekliyorum
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

            // veri tabanına kayıt işleminin başarısını kontrol ediyorum
            if (result == 0)
            {
                return BadRequest("An error occurred");
            }
            else
            {
                return Ok("Successful");
            }
        }

        //şu anki günün haftanın kaçıncı günü olduğunu hesaplıyorum
        private int GetWeekNumberOfMonth(DateTime date)
        {
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            DayOfWeek firstDayOfWeek = DayOfWeek.Monday;
            int weekNumber = (date.Day + GetDayOfWeekOffset(firstDayOfMonth, firstDayOfWeek) - 1) / 7 + 1;
            return weekNumber;
        }

        // şu anki günün haftanın ilk gününe göre offset değerini hesaplıyorum
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
