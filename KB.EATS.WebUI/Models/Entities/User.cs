using System;
using System.Collections.Generic;

namespace KB.EATS.WebUI.Models.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Password { get; set; }

    public bool? IsAdmin { get; set; }

    public DateTime? RegisterDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<UserShift> UserShifts { get; set; } = new List<UserShift>();

    public virtual UserStatistic? UserStatistic { get; set; }
}
