
namespace Simeserva.Services.Policies;

public interface IPoliciesService
{
	public Task<IImmutableList<Policy>> GetAll(CancellationToken ct);
}
