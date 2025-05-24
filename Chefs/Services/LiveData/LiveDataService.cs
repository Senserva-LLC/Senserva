
namespace Simeserva.Services.LiveData;

public class LiveDataService : ILiveDataService
{
	public async ValueTask<IImmutableList<LiveDataModel>> GetAll(CancellationToken ct)
	{
		return Get();
	}

	internal IImmutableList<LiveDataModel> Get() => new List<LiveDataModel>() { new LiveDataModel()}.ToImmutableList();

}
