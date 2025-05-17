namespace Chefs.Business.Models;

public partial record Cookbook : ISenservaEntity
{
	internal Cookbook() { Recipes = ImmutableList<Recipe>.Empty; }
	public Cookbook(IImmutableList<Recipe> recipes)
	{
		Recipes = recipes;
	}

	public Guid Id { get; init; }
	public Guid UserId { get; init; }
	public string? Name { get; init; }
	public int PinsNumber => Recipes?.Count ?? 0;
	public IImmutableList<Recipe> Recipes { get; init; }
	public CookbookImages? CookbookImages { get; init; }

	internal static Cookbook CreateData(Guid userId, string name, IImmutableList<Recipe> recipes)
	{
		return new Cookbook
		{
			Id = Guid.NewGuid(),
			Name = name,
			UserId = userId,
			Recipes = recipes
		};
	}

	internal UpdateCookbook UpdateCookbook() => new(this);
}
