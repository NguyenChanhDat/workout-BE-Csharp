using Microsoft.EntityFrameworkCore;
using dotenv.net;

class DatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        DotEnv.Load();

        string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "";
        optionsBuilder.UseSqlServer(connectionString);
    }

    public virtual DbSet<BodyTrack> BodyTracks { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Set> Sets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BodyTrack>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BodyTrac__3213E83F9376C573");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.User).WithMany(p => p.BodyTracks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BodyTracks_Users");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Exercise__3213E83F5F5DCFD7");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("imageUrl");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.TargetMuscle1)
                .HasMaxLength(50)
                .HasColumnName("targetMuscle1");
            entity.Property(e => e.TargetMuscle2)
                .HasMaxLength(50)
                .HasColumnName("targetMuscle2");
            entity.Property(e => e.TargetMuscle3)
                .HasMaxLength(50)
                .HasColumnName("targetMuscle3");
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Plans__3213E83FDE6669F9");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MembershipTier)
                .HasMaxLength(50)
                .HasColumnName("membershipTier");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Plans)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Plans_Users");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Session__3213E83F0BBAD9DE");

            entity.ToTable("Session");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.PlanId).HasColumnName("planId");

            entity.HasOne(d => d.Plan).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.PlanId)
                .HasConstraintName("FK_Session_Plans");
        });

        modelBuilder.Entity<Set>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sets__3213E83F473FF2E9");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExerciseId).HasColumnName("exerciseId");
            entity.Property(e => e.Reps).HasColumnName("reps");
            entity.Property(e => e.RestTime).HasColumnName("restTime");
            entity.Property(e => e.SessionId).HasColumnName("sessionId");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Exercise).WithMany(p => p.Sets)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sets_Exercises");

            entity.HasOne(d => d.Session).WithMany(p => p.Sets)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sets_Session");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83FE8E97ABC");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MembershipTier)
                .HasMaxLength(50)
                .HasDefaultValue("Basic")
                .HasColumnName("membershipTier");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

    }
}
