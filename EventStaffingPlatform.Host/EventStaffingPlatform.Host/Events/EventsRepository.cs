namespace EventStaffingPlatform.Host.Events;

public class EventsRepository(EventDbContext context) : IEventsRepository
{
	public async Task<bool> CreateEventAsync(EventStorageEntity entity)
	{
		await context.AddAsync(entity);
		return await context.SaveChangesAsync() > 0;
	}
}
