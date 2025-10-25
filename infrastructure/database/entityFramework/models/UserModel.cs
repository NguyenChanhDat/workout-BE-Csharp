using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder
            .Property(u => u.Email)
            .IsRequired();

        builder
            .Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}