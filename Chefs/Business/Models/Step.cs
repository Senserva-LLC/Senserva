
namespace Chefs.Business.Models;

public record Step
{
	public Step()
	{
		Cookware = ImmutableList<string>.Empty;
		Ingredients = ImmutableList<string>.Empty;
		CookTime = TimeSpan.Zero;
	}

	public int Number { get; init; }
	public string? Name { get; init; }
	public TimeSpan CookTime { get; init; }
	public IImmutableList<string>? Cookware { get; init; }
	public IImmutableList<string>? Ingredients { get; init; }
	public string? Description { get; init; }
	public string? UrlVideo { get; init; }

}
