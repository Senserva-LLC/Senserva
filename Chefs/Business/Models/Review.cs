
namespace Chefs.Business.Models;

public enum ComplianceType
{
	CVE, // CVE ID
}

public partial record Compliance
{
	public Compliance()
	{
		Id = Guid.NewGuid();
		TechniqueId = Guid.Empty;
		CreatedBy = Guid.Empty;
		Date = DateTimeOffset.MinValue;
	}

	public Compliance(Guid recipeId, string text)
	{
		Id = Guid.NewGuid();
		TechniqueId = recipeId;
		Description = text;
	}

	public Guid Id { get; init; }
	public Guid TechniqueId { get; init; }
	public string? UrlAuthorImage { get; init; }
	public Guid CreatedBy { get; init; }
	public string? PublisherName { get; init; }
	public DateTimeOffset Date { get; init; }
	public string? Description { get; init; }
	public ImmutableList<Guid>? Likes { get; init; }
	public ImmutableList<Guid>? Dislikes { get; init; }
	public bool UserLike { get; init; }
	public ComplianceType Type { get; init; } = ComplianceType.CVE;

}
