namespace Chefs.Business.Models;

/// <summary>
/// was Risks
/// </summary>
public partial record Risks
{
//	public double? Protein { get; }
	//public double? ProteinBase { get; }

	public double? DataRisk { get; }
	public double? DataRiskBase { get; }

	//public double? Carbs { get; }
	//public double? CarbsBase { get; }

	public double? UserRisk { get; }
	public double? UserRiskBase { get; }

	public double? DeviceRisk { get; }
	public double? DeviceRiskBase { get; }
}
