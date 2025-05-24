namespace Simeserva.Services.Policies;

public class PoliciesService : IPoliciesService
{
	public async Task<IImmutableList<Policy>> GetAll(Technique technique, CancellationToken ct)
	{
		return new List<Policy>() { new Policy()}.ToImmutableList();
	}

}
