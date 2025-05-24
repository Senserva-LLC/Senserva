namespace Simeserva.Business.Models;

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

	public Risks()
	{
		DataRisk = 10;
		DataRiskBase = 3;
		UserRisk = 4;
		UserRiskBase = 2;
		DeviceRisk = 6;
		DeviceRiskBase = 1;
	}
}
