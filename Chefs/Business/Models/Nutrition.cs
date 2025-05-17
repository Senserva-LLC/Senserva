namespace Chefs.Business.Models;

public partial record Nutrition
{
	public double? Protein { get; }
	public double? ProteinBase { get; }
	public double? Carbs { get; }
	public double? CarbsBase { get; }
	public double? Fat { get; }
	public double? FatBase { get; }
}
