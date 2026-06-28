using Microsoft.EntityFrameworkCore;

namespace EventStaffingPlatform.Host;

public class PositionsDbContext(DbContextOptions options) : DbContext(options)
{
	public DbSet<Position> Vacancies { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfiguration(new PositionEntityConfiguration());
	}
}
