
using Siemserva.Business.Models;

namespace Siemserva.Services.Targets;

public class TargetService : ITargetService
{
	public async ValueTask<IImmutableList<Target>> GetAll(CancellationToken ct)
	{
		var list = new List<Target>();

		list.Add(new Target());

		return list.ToImmutableList();
	}

}
