namespace EventStaffingPlatform.Host.Events;

public interface IEventsService
{
	public Task<bool> CreateNewEvent(EventDto eventDto);
}
