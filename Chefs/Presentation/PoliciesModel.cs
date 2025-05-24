
using Simeserva.Services.Policies;

namespace Simeserva.Presentation;

public partial record PoliciesModel
{
	private readonly IPoliciesService _service;
	private readonly Technique _technique;

	public PoliciesModel(Technique technique, IPoliciesService service)
	{
		_technique = technique;
		_service = service;
	}

	public IListFeed<Policy> Policies => ListFeed.Async(async ct => await _service.GetAll(_technique, ct));
}

