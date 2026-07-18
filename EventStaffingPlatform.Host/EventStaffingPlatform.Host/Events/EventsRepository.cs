using Microsoft.EntityFrameworkCore;

namespace EventStaffingPlatform.Host.Events;

public class EventsRepository(EventDbContext context) : IEventsRepository
{
	public async Task<bool> CreateEventAsync(EventStorageEntity entity)
	{
		await context.AddAsync(entity);
		return await context.SaveChangesAsync() > 0;
	}

	public async Task<EventStorageEntity[]> GetAllEventsAsync(CancellationToken cancellationToken)
	{
		return await context.Events.ToArrayAsync(cancellationToken);
	}

	public async Task<EventStorageEntity[]> GetActualEventsAsync(DateTimeOffset currentDate, CancellationToken cancellationToken)
	{
		return await context.Events.Where(e => e.StartEventDate.Date > currentDate.Date)
			.ToArrayAsync(cancellationToken);
	}

	public async Task<EventStorageEntity?> GetEventByIdAsync(int id, CancellationToken cancellationToken)
	{
		return await context.Events.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
	}

	public async Task UpdateEventAsync(EventStorageEntity entity, CancellationToken cancellationToken)
	{
		context.Events.Update(entity);
		await context.SaveChangesAsync(cancellationToken);
	}
}
