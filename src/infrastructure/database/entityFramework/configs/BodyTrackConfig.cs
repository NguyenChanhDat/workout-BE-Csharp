using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BodyTrackEntityTypeConfiguration : IEntityTypeConfiguration<BodyTrack>
{
    public void Configure(EntityTypeBuilder<BodyTrack> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__BodyTrac__3213E83F9376C573");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Date).HasColumnName("date");
        builder.Property(e => e.Height).HasColumnName("height");
        builder.Property(e => e.UserId).HasColumnName("userId");
        builder.Property(e => e.Weight).HasColumnName("weight");

        builder.HasOne(d => d.User).WithMany(p => p.BodyTracks)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_BodyTracks_Users");

        builder.HasData(
            new BodyTrack
            {
                Id = 1,
                UserId = 1,
                Date = new DateOnly(2025, 1, 1),
                Height = 186,
                Weight = 71.1
            },
            new BodyTrack
            {
                Id = 2,
                UserId = 2,
                Date = new DateOnly(2025, 1, 1),
                Height = 175,
                Weight = 68.5
            }
        );

    }
}