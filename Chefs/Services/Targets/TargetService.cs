
using Siemserva.Business.Models;

namespace Siemserva.Services.Target;

public class TargetService : ITargetService
{
	public async ValueTask<IImmutableList<Targets>> GetAll(CancellationToken ct)
	{
		var list = new List<Targets>();

		list.Add(new Business.Models.Targets());

		return list.ToImmutableList();
	}

}
