using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventStaffingPlatform.Host;

public class VacanciesDbContext(DbContextOptions options) : DbContext(options)
{
	public DbSet<Vacancy> Vacancies { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfiguration(new VacancyEntityConfiguration());
	}
}

public class VacancyEntityConfiguration : IEntityTypeConfiguration<Vacancy>
{
	public void Configure(EntityTypeBuilder<Vacancy> builder)
	{
		builder.ToTable("vacancies");

		builder.Property(v => v.Title)
			.HasMaxLength(1000)
			.HasColumnName("title")
			.IsRequired();

		builder.Property(v => v.Description)
			.HasColumnName("description");

		builder.Property(v => v.Requirements)
			.HasColumnName("requirements")
			.IsRequired();

		builder.Property(v => v.Functions)
			.HasColumnName("functions")
			.IsRequired();

		builder.Property(v => v.Conditions)
			.HasColumnName("conditions")
			.IsRequired();

		builder.Property(v => v.CreateDate)
			.HasColumnName("create_date")
			.IsRequired();

		builder.Property(v => v.CloseDate)
			.HasColumnName("close_date");

		builder.Property(v => v.ExpirationDate)
			.HasColumnName("expiration_date");

		builder.Property(v => v.Status)
			.HasColumnName("status")
			.HasConversion<string>();
	}
}
