

namespace Siemserva.Business.Models;

/// <summary>
/// domain, ip range, ip address, machine name
/// TODO figure out creds when on desktop etc
/// </summary>
public partial record Target : ISenservaEntity
{
	public Guid Id { get; init; }
	public string Name { get; init; }

	public Target()
	{
		Id = Guid.NewGuid();
		Name = "test2";
	}
}
