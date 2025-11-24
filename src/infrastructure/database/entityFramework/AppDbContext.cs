using Microsoft.EntityFrameworkCore;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public virtual DbSet<BodyTrack> BodyTracks { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Set> Sets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new BodyTrackEntityTypeConfiguration().Configure(modelBuilder.Entity<BodyTrack>());
        new ExerciseEntityTypeConfiguration().Configure(modelBuilder.Entity<Exercise>());
        new PlanEntityTypeConfiguration().Configure(modelBuilder.Entity<Plan>());
        new SessionEntityTypeConfiguration().Configure(modelBuilder.Entity<Session>());
        new SetEntityTypeConfiguration().Configure(modelBuilder.Entity<Set>());
        new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
    }
}
