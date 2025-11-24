using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__Users__3213E83FE8E97ABC");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.MembershipTier)
            .HasConversion<string>()
            .HasMaxLength(50)
            .HasDefaultValue(MembershipTierEnum.Basic)
            .HasColumnName("membershipTier")
            .IsRequired();
        builder.Property(e => e.Password)
            .HasMaxLength(255)
            .HasColumnName("password")
            .IsRequired();
        builder.Property(e => e.Username)
            .HasMaxLength(255)
            .HasColumnName("username")
            .IsRequired();
        builder.Property(e => e.Email)
            .HasMaxLength(100)
            .HasColumnName("email")
            .IsRequired();
    }
}