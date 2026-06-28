namespace EventStaffingPlatform.Host;

/// <summary>
/// Вакансия
/// </summary>
public class Position
{
	/// <summary>
	/// Идентификатор
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Заголовок вакансии
	/// </summary>
	public required string Title { get; set; }

	/// <summary>
	/// Описание вакансии
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Обязанности
	/// </summary>
	public required string Requirements { get; set; }

	/// <summary>
	/// Условия
	/// </summary>
	public required string Conditions { get; set; }

	/// <summary>
	/// Дата создания вакансии
	/// </summary>
	public DateTimeOffset CreateDate { get; set; }

	/// <summary>
	/// Дата обновления вакансии
	/// </summary>
	public DateTimeOffset? UpdateDate { get; set; }

	/// <summary>
	/// Статус вакансии
	/// </summary>
	public PositionStatus Status { get; set; }
}
