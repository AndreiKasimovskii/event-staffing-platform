using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventStaffingPlatform.Host.Conditions;

public class PositionsConditionTemplatesEntityConfiguration : IEntityTypeConfiguration<ConditionTemplate>
{
	public void Configure(EntityTypeBuilder<ConditionTemplate> builder)
	{
		builder.ToTable("position_condition_templates");

		builder.Property(c => c.Name)
			.HasMaxLength(64)
			.HasColumnName("name")
			.IsRequired();

		builder.Property(c => c.Caption)
			.HasMaxLength(256)
			.HasColumnName("caption")
			.IsRequired();

		builder.Property(c => c.Template)
			.HasColumnName("template")
			.HasColumnType("jsonb")
			.IsRequired();

		builder.HasIndex(c => c.Name).IsUnique();
	}
}
