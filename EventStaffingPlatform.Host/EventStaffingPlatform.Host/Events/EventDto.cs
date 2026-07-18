namespace EventStaffingPlatform.Host.Events;

public record EventDto(string Title, string? Description, string Address, DateTimeOffset StartEventDate, DateTimeOffset EndEventDate);

public record EventListItemResponse(int Id, string Title, DateTimeOffset StartEventDate, DateTimeOffset EndEventDate);

public record EventDetailsResponse(int Id, string Title, string? Description, string Address, DateTimeOffset StartEventDate, DateTimeOffset EndEventDate);