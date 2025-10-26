public class Exercise
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public string? TargetMuscle1 { get; set; }

    public string? TargetMuscle2 { get; set; }

    public string? TargetMuscle3 { get; set; }

    public virtual ICollection<Set> Sets { get; set; } = new List<Set>();
}
