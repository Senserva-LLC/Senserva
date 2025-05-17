
namespace Chefs.Business.Models;

/// <summary>
/// TODO rename this as Solution
/// </summary>
public partial record Recipe : ISenservaEntity
{
	public Guid Id { get; init; }
	public Guid UserId { get; init; }

	public string? ImageUrl { get; init; }
	public string? Name { get; init; }

	/// <summary>
	/// TODO can we get this reliability?
	/// </summary>
	public int ItemsEffected { get; init; } = 99;

	public TimeSpan EstimateTime { get; init; }
	public Difficulty Difficulty { get; init; }

	/// <summary>
	/// over all risks
	/// </summary>
	public OverallRisk Risk { get; init; }

	public Category Category { get; init; }
	public DateTimeOffset Date { get; init; }
	public bool IsFavorite { get; init; }
	public Risks Risks { get; init; }
	public List<Step>? Steps { get; set; }
	public List<Content>? Details { get; set; }
	public List<Review>? Reviews { get; set; }

	public Recipe()
	{
		Id = Guid.NewGuid();
		UserId = Guid.NewGuid();
		Name = "test";
		Date = DateTimeOffset.UtcNow;
		Risk = OverallRisk.High;
		Risks = new Risks();
		Category = new Category();
		Difficulty = Difficulty.Beginner;
		EstimateTime = TimeSpan.FromMinutes(30);
		Details = [new Content { Name = "bob" }];
	}

	/// <summary>
	/// TODO  use the better enum formatter
	/// </summary>
	public string RiskAmount => Risk.ToString();

	public string TimeCal
	{
		get
		{
			return EstimateTime > TimeSpan.FromHours(1)
				? $"{EstimateTime:%h} hour {EstimateTime:%m} mins • {Risk}"
				: $"{EstimateTime:%m} mins • {Risk}";
		}
	}

}
