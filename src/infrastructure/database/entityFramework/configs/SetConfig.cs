using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SetEntityTypeConfiguration : IEntityTypeConfiguration<Set>
{
    public void Configure(EntityTypeBuilder<Set> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__Sets__3213E83F473FF2E9");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.ExerciseId).HasColumnName("exerciseId");
        builder.Property(e => e.Reps).HasColumnName("reps");
        builder.Property(e => e.RestTime).HasColumnName("restTime");
        builder.Property(e => e.SessionId).HasColumnName("sessionId");
        builder.Property(e => e.Weight).HasColumnName("weight");

        builder.HasOne(d => d.Exercise).WithMany(p => p.Sets)
            .HasForeignKey(d => d.ExerciseId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Sets_Exercises");

        builder.HasOne(d => d.Session).WithMany(p => p.Sets)
            .HasForeignKey(d => d.SessionId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Sets_Session");

        builder.HasData(
            new Set
            {
                Id = 1,
                ExerciseId = 1,
                SessionId = 1,
                Reps = 10,
                Weight = 60,
                RestTime = 90
            },
            new Set
            {
                Id = 2,
                ExerciseId = 1,
                SessionId = 1,
                Reps = 8,
                Weight = 65,
                RestTime = 120
            },
            new Set
            {
                Id = 3,
                ExerciseId = 2,
                SessionId = 2,
                Reps = 12,
                Weight = 80,
                RestTime = 120
            }
        );

    }
}