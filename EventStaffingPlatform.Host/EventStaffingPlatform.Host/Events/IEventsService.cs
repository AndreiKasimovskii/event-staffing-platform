namespace EventStaffingPlatform.Host.Events;

public interface IEventsService
{
	Task<bool> CreateNewEvent(CreateEventRequest newEvent);

	/// <summary>
	/// Получить все мероприятия
	/// </summary>
	/// <param name="cancellationToken">Токен отмены выполнения операции</param>
	/// <returns>Мероприятия</returns>
	Task<EventListItemResponse[]> GetAllEventsAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Получить актуальные мероприятия
	/// </summary>
	/// <param name="cancellationToken">Токен отмены операции</param>
	/// <returns>Актуальные мероприятия</returns>
	Task<EventListItemResponse[]> GetActualEventsAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Получить данные мероприятия
	/// </summary>
	/// <param name="id">Идентификатор мероприятия</param>
	/// <param name="cancellationToken">Токен отмены операции</param>
	/// <returns>Данные мероприятия</returns>
	Task<GettingEventResult> GetEventAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Редактирование мероприятия
	/// </summary>
	/// <param name="id">Идентификатор мероприятия</param>
	/// <param name="request">Отредактированные данные мероприятия из запроса</param>
	/// <param name="cancellationToken">Токен отмены операции</param>
	/// <returns>Результат выполнения операции редактирования</returns>
	Task<EditingEventResult> EditEventAsync(int id, EditEventRequest request, CancellationToken cancellationToken);
}
