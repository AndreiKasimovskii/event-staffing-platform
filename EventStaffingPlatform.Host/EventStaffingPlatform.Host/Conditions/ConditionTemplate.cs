namespace EventStaffingPlatform.Host.Conditions;

public class ConditionTemplate
{
	public int Id { get; set; }

	public required string Name { get; set; }

	public required string Caption { get; set; }

	public required string Template { get; set; }
}
