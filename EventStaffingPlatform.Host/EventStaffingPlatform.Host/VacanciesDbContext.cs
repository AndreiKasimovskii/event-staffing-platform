using Microsoft.EntityFrameworkCore;

namespace EventStaffingPlatform.Host;

public class VacanciesDbContext(DbContextOptions options) : DbContext(options)
{
	public DbSet<Vacancy> Vacancies { get; set; }

	public DbSet<Requirement> Requirements { get; set; }

	public DbSet<RequirementType> RequirementTypes { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfiguration(new VacancyEntityConfiguration());
	}
}
