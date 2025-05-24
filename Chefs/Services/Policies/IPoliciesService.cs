
namespace Simeserva.Services.Policies;

public interface IPoliciesService
{
	public Task<IImmutableList<Policy>> GetAll(Technique technique, CancellationToken ct);
}
