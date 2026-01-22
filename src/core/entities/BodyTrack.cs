public class BodyTrack
{
    public int Id { get; set; }

    public double Weight { get; set; }

    public double Height { get; set; }

    public DateOnly Date { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
