using System.Text.Json;

namespace EventStaffingPlatform.Host;

public class PositionService(PositionsDbContext positionsDbContext) : IPositionService
{
	public async Task CreatePosition(Position position)
	{
		positionsDbContext.Add(position);
		await positionsDbContext.SaveChangesAsync();
	}
}

public interface IPositionService
{
	Task CreatePosition(Position position);
}