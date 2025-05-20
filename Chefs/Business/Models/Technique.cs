
namespace Chefs.Business.Models;

/// <summary>
/// TODO build out
/// An IT security control is a measure designed to protect information systems, data, and other assets from threats and vulnerabilities.
/// </summary>
/// <param name="Name"></param>
/// <param name="Description"></param>
public record SecurityControl(string Name, string Description);

/// <summary>
/// TODO rename this as Solution
/// TODO add a Rules table that maps to this data, and include it in the UI
/// </summary>
public partial record Technique : ISenservaEntity
{
	public Guid Id { get; init; }
	public Guid UserId { get; init; }

	public string? ImageUrl { get; init; }
	public string? Name { get; init; }
	public TechniqueType Type { get; init; } = TechniqueType.Remediation;

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

	/// <summary>
	/// user defined list of details displayed in the main part of the ui
	/// </summary>
	public List<string>? KeyDetails { get; set; }

	public List<RemediationStep>? Steps { get; set; }
	public List<Content>? Content { get; set; }
	public List<Compliance>? Compliance { get; set; }
	public List<SecurityControl>? Controls { get; set; }

	public Technique()
	{
		Id = Guid.NewGuid();
		UserId = Guid.NewGuid();
		Name = "test";
		Date = DateTimeOffset.UtcNow;
		Risk = OverallRisk.High;
		Risks = new Risks();
		Category = new Category();
		Difficulty = Difficulty.Basic;
		EstimateTime = TimeSpan.FromMinutes(30);
		Controls = [new SecurityControl("Bob", "syouruncle")];
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
