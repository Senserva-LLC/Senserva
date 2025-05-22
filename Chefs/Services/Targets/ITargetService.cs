
using Siemserva.Business.Models;
namespace Siemserva.Services.Target;

public interface ITargetService
{
	ValueTask<IImmutableList<Targets>> GetAll(CancellationToken ct);
}

