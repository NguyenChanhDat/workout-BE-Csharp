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
    }
}