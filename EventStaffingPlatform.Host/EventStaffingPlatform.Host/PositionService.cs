using System.Text.Json;

namespace EventStaffingPlatform.Host;

public class PositionService(PositionsDbContext positionsDbContext) : IPositionService
{
	public async Task CreatePosition(Position position)
	{
		if (string.IsNullOrWhiteSpace(position.Title))
			throw new Exception("Title can not be empty!");
		var conditions = JsonSerializer.Deserialize<Condition[]>(position.Conditions);

		var workingDaysCondition = conditions?.FirstOrDefault(c => c.Name == "working days");
		var workingHoursCondition = conditions?.FirstOrDefault(c => c.Name == "working hours");
		var rateCondition = conditions?.FirstOrDefault(c => c.Name == "rate");

		if (workingDaysCondition is null || workingHoursCondition is null || rateCondition is null)
			throw new Exception("Job prerequisites are not listed");

		positionsDbContext.Add(position);
		await positionsDbContext.SaveChangesAsync();
	}
}

public interface IPositionService
{
	Task CreatePosition(Position position);
}

public sealed class Condition
{
	public string? Name { get; set; }

	public string? Value { get; set; }
}