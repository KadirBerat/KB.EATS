﻿using System;
using System.Collections.Generic;

namespace KB.EATS.WebUI.Models.Entities;

public partial class UserStatistic
{
    public int UserStatisticId { get; set; }

    public int UserId { get; set; }

    public virtual User UserStatisticNavigation { get; set; } = null!;

    public virtual ICollection<Year> Years { get; set; } = new List<Year>();
}
