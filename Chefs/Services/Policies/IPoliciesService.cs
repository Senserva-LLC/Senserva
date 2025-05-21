
namespace Simeserva.Services.LiveData;

public interface IPoliciesService
{
	public Task<IImmutableList<Policy>> GetAll(CancellationToken ct);
}
