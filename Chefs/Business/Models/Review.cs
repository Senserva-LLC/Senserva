
namespace Chefs.Business.Models;

public enum ReviewType
{
	CVE, // CVE ID
}

public partial record Review
{
	public Review()
	{
		Id = Guid.NewGuid();
		RecipeId = Guid.Empty;
		CreatedBy = Guid.Empty;
		Date = DateTimeOffset.MinValue;
	}

	public Review(Guid recipeId, string text)
	{
		Id = Guid.NewGuid();
		RecipeId = recipeId;
		Description = text;
	}

	public Guid Id { get; init; }
	public Guid RecipeId { get; init; }
	public string? UrlAuthorImage { get; init; }
	public Guid CreatedBy { get; init; }
	public string? PublisherName { get; init; }
	public DateTimeOffset Date { get; init; }
	public string? Description { get; init; }
	public ImmutableList<Guid>? Likes { get; init; }
	public ImmutableList<Guid>? Dislikes { get; init; }
	public bool UserLike { get; init; }
	public ReviewType Type { get; init; } = ReviewType.CVE;

}
