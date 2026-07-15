namespace EventStaffingPlatform.Host.Events;

public interface IEventsService
{
	Task<bool> CreateNewEvent(EventDto eventDto);
}
