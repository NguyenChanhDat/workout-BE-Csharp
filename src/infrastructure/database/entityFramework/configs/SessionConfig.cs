using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SessionEntityTypeConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__Session__3213E83F0BBAD9DE");

        builder.ToTable("Session");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Date).HasColumnName("date");
        builder.Property(e => e.PlanId).HasColumnName("planId");

        builder.HasOne(d => d.Plan).WithMany(p => p.Sessions)
            .HasForeignKey(d => d.PlanId)
            .HasConstraintName("FK_Session_Plans");

        builder.HasData(
            new Session
            {
                Id = 1,
                Date = new DateOnly(2025, 1, 10),
                PlanId = 1
            },
            new Session
            {
                Id = 2,
                Date = new DateOnly(2025, 1, 12),
                PlanId = 2
            }
        );

    }
}