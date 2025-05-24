namespace Simeserva.Business.Models;

public record Notification
{
	internal Notification()
	{
		Date = DateTimeOffset.MinValue;
	}

	public string? Title { get; init; }
	public string? Description { get; init; }
	public bool Read { get; init; }
	public DateTimeOffset Date { get; init; }
}
