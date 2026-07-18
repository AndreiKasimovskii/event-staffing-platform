namespace EventStaffingPlatform.Host.Events;

public class EventsService(IEventsRepository eventsRepository) : IEventsService
{
	public async Task<bool> CreateNewEvent(CreateEventRequest newEvent)
	{
		if (string.IsNullOrWhiteSpace(newEvent.Title))
			throw new ArgumentException("Event title cannot be empty or contains only space!", nameof(newEvent.Title));

		if(string.IsNullOrWhiteSpace(newEvent.Address))
			throw new ArgumentException("Event address cannot be empty or contains only space!", nameof(newEvent.Address));

		if (newEvent.StartEventDate.CompareTo(newEvent.EndEventDate) > 0)
			throw new ArgumentException("Event end date cannot be earlier than start date!", nameof(newEvent.EndEventDate));

		if (newEvent.StartEventDate.UtcDateTime.Date.CompareTo(DateTime.UtcNow.Date) < 0)
			throw new ArgumentException("Event start date cannot be earlier than today", nameof(newEvent.StartEventDate));

		EventStorageEntity eventStorageEntity = new()
		{
			Title = newEvent.Title,
			Description = newEvent.Description,
			Address = newEvent.Address,
			StartEventDate = newEvent.StartEventDate.ToUniversalTime(),
			EndEventDate = newEvent.EndEventDate.ToUniversalTime()
		};

		return await eventsRepository.CreateEventAsync(eventStorageEntity);
	}

	public async Task<EventListItemResponse[]> GetAllEventsAsync(CancellationToken cancellationToken)
	{
		var events = await eventsRepository.GetAllEventsAsync(cancellationToken);
		return [.. events.Select(e => new EventListItemResponse(e.Id, e.Title, e.StartEventDate, e.EndEventDate))];
	}

	public async Task<EventListItemResponse[]> GetActualEventsAsync(CancellationToken cancellationToken)
	{
		var actualEvents = await eventsRepository.GetActualEventsAsync(DateTimeOffset.UtcNow, cancellationToken);
		return [.. actualEvents.Select(e => new EventListItemResponse(e.Id, e.Title, e.StartEventDate, e.EndEventDate))];
	}

	public async Task<GettingEventResult> GetEventAsync(int id, CancellationToken cancellationToken)
	{
		var concreteEvent = await eventsRepository.GetEventByIdAsync(id, cancellationToken);
		if (concreteEvent == null)
			return new(false, null);
		return new(true, new(concreteEvent.Id, concreteEvent.Title, concreteEvent.Description, concreteEvent.Address, concreteEvent.StartEventDate, concreteEvent.EndEventDate));
	}
}

public record GettingEventResult(bool IsSuccess, EventDetailsResponse? Event);