using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserStoreEntity>
{
    public void Configure(EntityTypeBuilder<UserStoreEntity> builder)
    {
        builder.ToTable("users")
            .HasKey(e => e.Id)
            .HasName("user_id");

        #region Properties

        builder.Property(u => u.FirstName)
            .HasMaxLength(128)
            .HasColumnName("first_name")
            .IsRequired();
        builder.Property(u => u.LastName)
            .HasMaxLength(256)
            .HasColumnName("last_name")
            .IsRequired();
        builder.Property(u => u.Sex)
            .HasColumnName("sex");
        builder.Property(u => u.Login)
            .HasMaxLength(128)
            .HasColumnName("login")
            .IsRequired();
        builder.Property(u => u.Password)
            .HasMaxLength(256)
            .HasColumnName("password")
            .IsRequired();
        builder.Property(u => u.Email)
            .HasColumnName("email");
        builder.Property(u => u.PhoneNumber)
            .HasColumnName("phone_number")
            .HasMaxLength(16);
        builder.Property(u => u.BirthDate)
            .HasColumnName("birth_date")
            .HasColumnType("date");

        #endregion

        #region Indexes

        builder.HasIndex(u => u.Login)
            .IsUnique();
        builder.HasIndex(u => u.Email)
            .HasFilter("email is not null")
            .IsUnique();
        builder.HasIndex(u => u.PhoneNumber)
            .HasFilter("phone_number is not null")
            .IsUnique();

        #endregion
    }
}