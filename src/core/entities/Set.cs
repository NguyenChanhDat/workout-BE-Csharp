public class Set
{
    public int Id { get; set; }

    public int SessionId { get; set; }

    public int ExerciseId { get; set; }

    public int Weight { get; set; }

    public int Reps { get; set; }

    public int RestTime { get; set; }

    public virtual Exercise Exercise { get; set; } = null!;

    public virtual Session Session { get; set; } = null!;
}
