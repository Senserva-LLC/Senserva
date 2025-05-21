
using Simeserva.Services.LiveData;

namespace Simeserva.Presentation;

public partial record PoliciesModel
{
	private readonly IPoliciesService _service;
	public Policy Policy { get; }

	public PoliciesModel(
		Policy policy,
		IPoliciesService service)
	{
		 Policy = policy;
		_service = service;
	}

	public IListFeed<Policy> Policies => ListFeed.Async(async ct => await _service.GetAll(ct));
}

