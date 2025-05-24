
namespace Simeserva.Business.Models;

public record RemediationStep
{
	public RemediationStep()
	{
		Cookware = ImmutableList<string>.Empty;
		Ingredients = ImmutableList<string>.Empty;
		EstimatedTime = TimeSpan.Zero;
		Name = "test";
	}

	public int Number { get; init; }
	public string? Name { get; init; }
	public TimeSpan EstimatedTime { get; init; }
	public IImmutableList<string>? Cookware { get; init; }
	public IImmutableList<string>? Ingredients { get; init; }
	public string? Description { get; init; }
	public string? UrlVideo { get; init; }

}
