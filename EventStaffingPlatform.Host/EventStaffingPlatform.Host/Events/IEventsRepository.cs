namespace EventStaffingPlatform.Host.Events;

public interface IEventsRepository
{
	Task<bool> CreateEventAsync(EventStorageEntity entity);

	/// <summary>
	/// Получение всех мероприятий
	/// </summary>
	/// <param name="cancellationToken">Токен отмены выполнения операции</param>
	/// <returns>Мероприятия</returns>
	Task<EventStorageEntity[]> GetAllEventsAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Получение акутальных мероприятий
	/// </summary>
	/// <param name="currentDate">Текущая дата</param>
	/// <param name="cancellationToken">Токен отмены операции</param>
	/// <returns>Актуальные мероприятия</returns>
	Task<EventStorageEntity[]> GetActualEventsAsync(DateTimeOffset currentDate, CancellationToken cancellationToken);

	/// <summary>
	/// Получение мероприятия по идентификатору
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <param name="cancellationToken">Токен отмены операции</param>
	/// <returns>Данные мероприятия</returns>
	Task<EventStorageEntity?> GetEventByIdAsync(int id, CancellationToken cancellationToken);
}
