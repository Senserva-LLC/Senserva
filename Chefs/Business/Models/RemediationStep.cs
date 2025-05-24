
namespace Simeserva.Business.Models;

/// <summary>
///  TODO build out the wizard and make the button clear
/// </summary>
public record RemediationStep
{
	public RemediationStep()
	{
		EstimatedTime = TimeSpan.Zero;
		Name = "test";
	}

	public int Number { get; init; }
	public string? Description { get; init; }
	public string? Name { get; init; }
	public TimeSpan EstimatedTime { get; init; }
	public IImmutableList<Uri>? References { get; init; }
	public string? UrlVideo { get; init; }

}
