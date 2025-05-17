namespace Chefs.Business.Models;

/// <summary>
/// TODO I think its stuff like Intune but TBD for now
/// </summary>
public partial record Category
{
	public int? Id { get; init; }
	public string? UrlIcon { get; init; }
	public string? Name { get; init; }
	public string? Color { get; init; }
}
