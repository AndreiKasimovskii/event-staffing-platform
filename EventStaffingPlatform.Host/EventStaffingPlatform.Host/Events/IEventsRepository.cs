namespace EventStaffingPlatform.Host.Events;

public interface IEventsRepository
{
	Task<bool> CreateEvent(EventStorageEntity entity);
}
