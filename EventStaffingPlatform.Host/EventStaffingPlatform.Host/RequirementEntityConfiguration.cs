using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventStaffingPlatform.Host;

public class RequirementEntityConfiguration : IEntityTypeConfiguration<Requirement>
{
	public void Configure(EntityTypeBuilder<Requirement> builder)
	{
		builder.ToTable("requirements")
			.HasKey(r => new { r.RequirementTypeId, r.VacancyId });

		builder.HasOne(r => r.Vacancy)
			.WithMany(v => v.Requirements);

		builder.Property(r => r.RequirementTypeId)
			.HasColumnName("requirement_type_id")
			.IsRequired();

		builder.Property(r => r.VacancyId)
			.HasColumnName("vacancy_id")
			.IsRequired();

		builder.Property(r => r.Value)
			.HasColumnName("value")
			.HasColumnType("jsonb")
			.IsRequired();

		builder.HasIndex(r => r.RequirementTypeId);
		builder.HasIndex(r => r.VacancyId);
	}
}