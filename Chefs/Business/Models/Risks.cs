namespace Chefs.Business.Models;

/// <summary>
/// risk rating
/// </summary>
public enum OverallRisk
{
	NoneKnown,
	Low,
	Medium,
	High,
	Critical
}

/// <summary>
/// areas of risk by type
/// </summary>
public partial record Risks
{
	public double? DataRisk { get; }
	public double? DataRiskBase { get; }

	public double? UserRisk { get; }
	public double? UserRiskBase { get; }

	public double? DeviceRisk { get; }
	public double? DeviceRiskBase { get; }
}
