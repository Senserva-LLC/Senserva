namespace Simeserva.Services.LiveData;

public class PoliciesService : IPoliciesService
{
	public async Task<IImmutableList<Policy>> GetAll(CancellationToken ct)
	{
		return new List<Policy>() { new Policy()}.ToImmutableList();
	}

}
