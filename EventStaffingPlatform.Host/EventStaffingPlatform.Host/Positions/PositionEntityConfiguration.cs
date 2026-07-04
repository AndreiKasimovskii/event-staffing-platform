using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventStaffingPlatform.Host.Positions;

public class PositionEntityConfiguration : IEntityTypeConfiguration<Position>
{
	public void Configure(EntityTypeBuilder<Position> builder)
	{
		builder.ToTable("positions");

		builder.Property(v => v.Title)
			.HasMaxLength(1000)
			.HasColumnName("title")
			.IsRequired();

		builder.Property(v => v.Description)
			.HasColumnName("description");

		builder.Property(v => v.Requirements)
			.HasColumnName("requirements")
			.HasColumnType("jsonb");

		builder.Property(v => v.Conditions)
			.HasColumnName("conditions")
			.HasColumnType("jsonb")
			.IsRequired();

		builder.Property(v => v.CreateDate)
			.HasColumnName("create_date")
			.IsRequired();

		builder.Property(v => v.UpdateDate)
			.HasColumnName("update_date");

		builder.Property(v => v.Status)
			.HasColumnName("status")
			.HasConversion<string>()
			.IsRequired();
	}
}
