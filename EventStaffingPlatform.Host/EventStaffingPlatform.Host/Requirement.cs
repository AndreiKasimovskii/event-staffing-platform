namespace EventStaffingPlatform.Host;

public class Requirement
{
	public required int RequirementTypeId { get; set; }
	public RequirementType? RequirementType { get; set; }

	public int VacancyId { get; set; }
	public Vacancy? Vacancy { get; set; }

	public required string Value { get; set; }
}
