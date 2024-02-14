namespace KB.EATS.WebApi.Models.Entities;

public partial class UserStatistic
{
    public int UserStatisticId { get; set; }

    public int UserId { get; set; }

    public virtual User UserStatisticNavigation { get; set; } = null!;

    public virtual ICollection<Year> Years { get; set; } = new List<Year>();
}
