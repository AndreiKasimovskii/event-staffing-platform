using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventStaffingPlatform.Host;

public class RequirementTypeEntityConfiguration : IEntityTypeConfiguration<RequirementType>
{
	public void Configure(EntityTypeBuilder<RequirementType> builder)
	{
		builder.ToTable("requirement_types");

		builder.Property(rt => rt.Id)
			.HasColumnName("id");

		builder.Property(rt => rt.Name)
			.HasColumnName("name")
			.HasMaxLength(30)
			.IsRequired();

		builder.Property(rt => rt.Caption)
			.HasColumnName("caption")
			.HasMaxLength(128)
			.IsRequired();

		builder.Property(rt => rt.ValueType)
			.HasColumnName("value_type")
			.HasMaxLength(256)
			.IsRequired();

		builder.Property(rt => rt.ValueConfiguration)
			.HasColumnName("value_configuration")
			.HasColumnType("jsonb")
			.IsRequired();

		builder.HasIndex(rt => rt.Name)
			.IsUnique();

		builder.HasIndex(rt => rt.Caption)
			.IsUnique();
	}
}