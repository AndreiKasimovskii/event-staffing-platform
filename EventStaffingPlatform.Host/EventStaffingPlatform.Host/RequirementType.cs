namespace EventStaffingPlatform.Host;

public class RequirementType
{
	public int Id { get; set; }

	public required string Name { get; set; }

	public required string Caption { get; set; }

	public required string ValueType { get; set; }

	public required string ValueConfiguration { get; set; }
}
