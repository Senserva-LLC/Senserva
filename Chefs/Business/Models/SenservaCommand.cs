﻿namespace Simeserva.Business.Models;

/// <summary>
/// TODO rename this as Solution
/// TODO add a Rules table that maps to this data, and include it in the UI
/// </summary>
public partial record SenservaCommand : ISenservaEntity
{
	public Guid Id { get; init; }
	public Guid UserId { get; init; }

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

	public List<Content>? Content { get; set; }
	public List<Compliance>? Compliance { get; set; }
	public List<SecurityControl>? Controls { get; set; }

	public SenservaCommand()
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

}
