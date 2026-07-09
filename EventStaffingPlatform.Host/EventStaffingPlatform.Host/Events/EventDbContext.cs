using Microsoft.EntityFrameworkCore;

namespace EventStaffingPlatform.Host.Events;

public class EventDbContext(DbContextOptions options) : DbContext(options)
{
	public DbSet<EventStorageEntity> Events { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfiguration(new EventEntityConfiguration());
	}
}
