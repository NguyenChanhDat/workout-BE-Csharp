public class Session
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public int? PlanId { get; set; }

    public virtual Plan? Plan { get; set; }

    public virtual ICollection<Set> Sets { get; set; } = new List<Set>();
}
