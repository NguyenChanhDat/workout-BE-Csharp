using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PlanEntityTypeConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__Plans__3213E83FDE6669F9");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.MembershipTier)
            .HasMaxLength(50)
            .HasColumnName("membershipTier");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasColumnName("name");
        builder.Property(e => e.UserId).HasColumnName("userId");

        builder.HasOne(d => d.User).WithMany(p => p.Plans)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Plans_Users");
    }
}