public class Exercise
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public MuscleEnum? TargetMuscle1 { get; set; }

    public MuscleEnum? TargetMuscle2 { get; set; }

    public MuscleEnum? TargetMuscle3 { get; set; }

    public virtual ICollection<Set> Sets { get; set; } = new List<Set>();
}
