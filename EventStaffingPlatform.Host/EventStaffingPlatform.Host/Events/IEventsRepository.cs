namespace EventStaffingPlatform.Host.Events;

public interface IEventsRepository
{
	Task<bool> CreateEventAsync(EventStorageEntity entity);
}
