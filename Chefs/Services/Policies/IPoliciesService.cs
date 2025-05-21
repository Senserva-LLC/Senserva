
namespace Simeserva.Services.LiveData;

public interface IPoliciesService
{
	ValueTask<IImmutableList<LiveDataModel>> GetAll(CancellationToken ct);
}
