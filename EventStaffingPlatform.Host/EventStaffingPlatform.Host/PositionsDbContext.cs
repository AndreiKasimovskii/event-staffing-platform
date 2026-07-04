using EventStaffingPlatform.Host.Conditions;
using EventStaffingPlatform.Host.Positions;
using Microsoft.EntityFrameworkCore;

namespace EventStaffingPlatform.Host;

public class PositionsDbContext(DbContextOptions options) : DbContext(options)
{
	public DbSet<Position> Positions { get; set; }

	public DbSet<ConditionTemplate> PositionsConditionTemplates { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfiguration(new PositionEntityConfiguration());
	}
}
