public class BodyTrack
{
    public int Id { get; set; }

    public int Weight { get; set; }

    public int Height { get; set; }

    public DateOnly Date { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
