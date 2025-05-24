
namespace Simeserva.Business.Models;

/// <summary>
/// TODO rename this as Solution
/// TODO add a Rules table that maps to this data, and include it in the UI
/// </summary>
public partial record Report : ISenservaEntity
{
	public Guid Id { get; init; }
	public string? Name { get; init; }
	public string? Description { get; init; }
	public Guid TechniqueId { get; init; }

	public Report()
	{
		Id = Guid.NewGuid();
		Name = "test";
	}

}
