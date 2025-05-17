namespace Chefs.Business.Models;

/// <summary>
/// TODO add a matching set of rules
/// </summary>
public enum ManagedCategory
{
	IntuneDrift,
	IntunePatch,
	SenservaPatch,
	EntraUsers,
	WindowsUsers,
}

/// <summary>
/// </summary>
public partial record Category
{
	/// <summary>
	///  key
	/// </summary>
	public int? Id { get; init; }
	public ManagedCategory? Name { get; init; } = ManagedCategory.IntunePatch;
	public string? Color { get; init; }
}
