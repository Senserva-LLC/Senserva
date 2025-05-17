
namespace Chefs.Business.Models;

public record SearchFilter(
	FilterGroup? FilterGroup = null,
	FixTime? Time = null,
	Difficulty? Difficulty = null,
	int? ItemsEffected = null,
	Category? Category = null)
{
	public bool HasFilter => FilterGroup != null || Time != null ||
							 Difficulty != null || Category != null || ItemsEffected != null;

	public bool Match(Recipe recipe)
	{
		var maxTime = Time switch
		{
			FixTime.Under15min => TimeSpan.FromMinutes(15),
			FixTime.Under30min => TimeSpan.FromMinutes(30),
			FixTime.Under60min => TimeSpan.FromMinutes(60),
			FixTime.Under120min => TimeSpan.FromMinutes(120),
			_ => TimeSpan.MaxValue,
		};

		var timespan = recipe.EstimateTime;

		return (Difficulty == null || recipe.Difficulty == Difficulty) &&
			   (Time == null || timespan < maxTime) &&
			   (Category == null || recipe.Category.Id == Category.Id || recipe.Category.Name == Category.Name) &&
			   (ItemsEffected == null || ItemsEffected == recipe.ItemsEffected);
	}

}
