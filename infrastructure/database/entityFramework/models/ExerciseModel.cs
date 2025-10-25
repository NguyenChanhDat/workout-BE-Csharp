using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ExerciseEntityTypeConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.ToTable("Exercises");

        builder
            .Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(u => u.Description)
            .IsRequired()
            .HasMaxLength(500);
    }
}