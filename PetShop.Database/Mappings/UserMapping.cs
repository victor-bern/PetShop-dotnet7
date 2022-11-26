using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShop.Domain.Entities;

namespace PetShop.Database.Mappings;


public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .ValueGeneratedOnAdd();

        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(o => o.LastName)
            .HasMaxLength(150);

        builder.Property(o => o.Email)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(o => o.Password)
            .IsRequired();

        builder.Property(o => o.Role)
            .HasColumnType("tinyint");

        builder.Property(o => o.BirthDate)
            .HasColumnType("date");

    }
}