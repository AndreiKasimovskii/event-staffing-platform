namespace EventStaffingPlatform.Host.Events;

public record EventDto(string Title, string? Description, string Address, DateTimeOffset StartEventDate, DateTimeOffset EndEventDate);