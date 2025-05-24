namespace Simeserva.Business.Models;

public record SearchHistory
{
	public List<string> Searches { get; init; } = new();
}
