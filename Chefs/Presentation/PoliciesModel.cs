
using Simeserva.Services.LiveData;

namespace Simeserva.Presentation;

public partial record PoliciesModel
{
	private readonly IPoliciesService _service;

	public PoliciesModel(IPoliciesService service)
	{
		_service = service;
	}

	public IListFeed<Policy> Policies => ListFeed.Async(async ct => await _service.GetAll(ct));
}

