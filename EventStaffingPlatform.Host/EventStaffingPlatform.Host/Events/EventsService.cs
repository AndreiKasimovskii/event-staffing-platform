namespace EventStaffingPlatform.Host.Events;

public class EventsService(IEventsRepository eventsRepository) : IEventsService
{
	public async Task<bool> CreateNewEvent(EventDto eventDto)
	{
		if (string.IsNullOrWhiteSpace(eventDto.Title))
			throw new ArgumentException("Event title cannot be empty or contains only space!", nameof(eventDto.Title));

		if(string.IsNullOrWhiteSpace(eventDto.Address))
			throw new ArgumentException("Event address cannot be empty or contains only space!", nameof(eventDto.Address));

		if (eventDto.StartEventDate.CompareTo(eventDto.EndEventDate) > 0)
			throw new ArgumentException("Event end date cannot be earlier than start date!", nameof(eventDto.EndEventDate));

		if (eventDto.StartEventDate.UtcDateTime.Date.CompareTo(DateTime.UtcNow.Date) < 0)
			throw new ArgumentException("Event start date cannot be earlier than today", nameof(eventDto.StartEventDate));

		EventStorageEntity eventStorageEntity = new()
		{
			Title = eventDto.Title,
			Description = eventDto.Description,
			Address = eventDto.Address,
			StartEventDate = eventDto.StartEventDate.ToUniversalTime(),
			EndEventDate = eventDto.EndEventDate.ToUniversalTime()
		};

		return await eventsRepository.CreateEventAsync(eventStorageEntity);
	}
}
