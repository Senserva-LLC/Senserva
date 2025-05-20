namespace Chefs.Business.Models;

/// <summary>
/// content of item of interest, for events its event data,
/// for conditional access is conditional access data,
/// its just a string list now but we can add more as we build it out
/// </summary>
public record Content
{
	public string? Name { get; init; }
	public string? Value { get; init; }

	public Content(string name, string value)
	{
		Name = name;
		Value = value;
	}
}
