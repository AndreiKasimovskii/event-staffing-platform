namespace EventStaffingPlatform.Host.Events;

public class EventStorageEntity
{
	public int Id { get; set; }

	public required string Title { get; set; }

	public string? Description { get; set; }

	public required string Address { get; set; }

	public DateTimeOffset StartEventDate { get; set; }

	public DateTimeOffset EndEventDate { get; set; }
}
