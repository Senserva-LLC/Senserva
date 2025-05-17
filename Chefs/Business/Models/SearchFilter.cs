
namespace Chefs.Business.Models;

public record SearchFilter(
	FilterGroup? FilterGroup = null,
	Time? Time = null,
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
			Models.Time.Under15min => TimeSpan.FromMinutes(15),
			Models.Time.Under30min => TimeSpan.FromMinutes(30),
			Models.Time.Under60min => TimeSpan.FromMinutes(60),
			_ => TimeSpan.MaxValue,
		};

		var timespan = recipe.EstimateTime;

		return (Difficulty == null || recipe.Difficulty == Difficulty) &&
			   (Time == null || timespan < maxTime) &&
			   (Category == null || recipe.Category.Id == Category.Id || recipe.Category.Name == Category.Name) &&
			   (ItemsEffected == null || ItemsEffected == recipe.ItemsEffected);
	}

}
