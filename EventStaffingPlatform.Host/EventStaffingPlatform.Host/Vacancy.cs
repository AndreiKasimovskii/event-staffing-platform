namespace EventStaffingPlatform.Host;

/// <summary>
/// Вакансия
/// </summary>
public class Vacancy
{
	public int Id { get; set; }

	public required string Title { get; set; }

	public string? Description { get; set; }

	public required string Requirements { get; set; }

	public required string Functions { get; set; }

	public required string Conditions { get; set; }

	public DateTimeOffset CreateDate { get; set; }

	public DateTimeOffset? UpdateDate { get; set; }

	public DateTimeOffset? CloseDate { get; set; }

	public DateTimeOffset? ExpirationDate { get; set; }

	public VacancyStatus Status { get; set; }
}

/// <summary>
/// Статус вакансии
/// </summary>
public enum VacancyStatus
{
	Open = 1,
	Close = 2
}