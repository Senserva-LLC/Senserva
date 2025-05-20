namespace Chefs.Business.Models;

public partial record Cookbook : ISenservaEntity
{
	internal Cookbook() { Techniques = ImmutableList<Technique>.Empty; }
	public Cookbook(IImmutableList<Technique> recipes)
	{
		Techniques = recipes;
	}

	public Guid Id { get; init; }
	public Guid UserId { get; init; }
	public string? Name { get; init; }
	public int PinsNumber => Techniques?.Count ?? 0;
	public IImmutableList<Technique> Techniques { get; init; }

	internal static Cookbook CreateData(Guid userId, string name, IImmutableList<Technique> recipes)
	{
		return new Cookbook
		{
			Id = Guid.NewGuid(),
			Name = name,
			UserId = userId,
			Techniques = recipes
		};
	}

	internal UpdateCookbook UpdateCookbook() => new(this);
}
