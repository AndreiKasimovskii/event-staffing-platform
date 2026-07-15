namespace EventStaffingPlatform.Host.Events;

public class EventsService(IEventsRepository eventsRepository) : IEventsService
{
	public async Task<bool> CreateNewEvent(EventDto eventDto)
	{
		if (string.IsNullOrWhiteSpace(eventDto.Title))
			throw new Exception("Event title cannot be empty or contains only space!");

		if(string.IsNullOrWhiteSpace(eventDto.Address))
			throw new Exception("Event address cannot be empty or contains only space!");

		if (eventDto.StartEventDate.CompareTo(eventDto.EndEventDate) > 0)
			throw new Exception("Event start date cannot be later than end date!");

		if (eventDto.StartEventDate < DateTimeOffset.Now.Date)
			throw new Exception("Event start date cannot be earlier then today");

		EventStorageEntity eventStorageEntity = new()
		{
			Title = eventDto.Title,
			Description = eventDto.Description,
			Address = eventDto.Address,
			StartEventDate = eventDto.StartEventDate.UtcDateTime,
			EndEventDate = eventDto.EndEventDate.UtcDateTime
		};

		return await eventsRepository.CreateEventAsync(eventStorageEntity);
	}
}
