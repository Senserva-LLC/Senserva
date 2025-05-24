
namespace Simeserva.Business.Models;

/// <summary>
/// TODO rename this as Solution
/// TODO add a Rules table that maps to this data, and include it in the UI
/// </summary>
public partial record Policy : ISenservaEntity
{
	public Guid Id { get; init; }
	public string? Name { get; init; }
	public string? Description { get; init; }

	public Category Category { get; init; }
	public DateTimeOffset Created { get; init; }
	public bool IsFavorite { get; init; }

	public List<Content>? Content { get; set; }

	public Policy()
	{
		Id = Guid.NewGuid();
		Name = "Test Policy";
		Description = "Demo description";
		Created = DateTimeOffset.UtcNow;
		Category = new Category();
	}
}
