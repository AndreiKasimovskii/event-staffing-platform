using System.Text.Json;

namespace EventStaffingPlatform.Host.Positions;

public class PositionService(PositionsDbContext positionsDbContext) : IPositionService
{
	public async Task<CreatePositionResult> CreatePosition(PositionDto position)
	{
		try
		{
			if (string.IsNullOrWhiteSpace(position.Title))
				return new CreatePositionResult(false, "Title can't be empty!");
			var conditions = JsonSerializer.Deserialize<Condition[]>(position.Conditions);

			var workingDaysCondition = conditions?.FirstOrDefault(c => c.Name == "working days");
			var workingHoursCondition = conditions?.FirstOrDefault(c => c.Name == "working hours");
			var rateCondition = conditions?.FirstOrDefault(c => c.Name == "rate");

			if (workingDaysCondition is null || workingHoursCondition is null || rateCondition is null)
				return new CreatePositionResult(false, "Job prerequisites are not listed");

			Position newPosition = new()
			{
				Title = position.Title,
				Description = position.Description,
				Requirements = position.Requirements,
				Conditions = position.Conditions,
				CreateDate = DateTimeOffset.UtcNow,
				Status = PositionStatus.Open
			};

			positionsDbContext.Add(newPosition);
			await positionsDbContext.SaveChangesAsync();
			return new CreatePositionResult(true, null);
		}
		catch (Exception ex)
		{
			return new CreatePositionResult(false, ex.Message);
		}
	}
}

public interface IPositionService
{
	Task<CreatePositionResult> CreatePosition(PositionDto position);
}

public sealed class Condition
{
	public string? Name { get; set; }

	public string? Value { get; set; }
}

public record CreatePositionResult(bool IsSuccess, string? Error);