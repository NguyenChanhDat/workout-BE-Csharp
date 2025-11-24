using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ExerciseEntityTypeConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__Exercise__3213E83F5F5DCFD7");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.ImageUrl)
            .HasMaxLength(255)
            .HasColumnName("imageUrl");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasColumnName("name");
        builder.Property(e => e.TargetMuscle1)
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasColumnName("targetMuscle1");
        builder.Property(e => e.TargetMuscle2)
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasColumnName("targetMuscle2");
        builder.Property(e => e.TargetMuscle3)
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasColumnName("targetMuscle3");
    }
}