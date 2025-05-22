
using Siemserva.Business.Models;
using Siemserva.Services.Target;

namespace Simeserva.Presentation;

public partial record TargetsModel
{
	private readonly ITargetService _service;

	public TargetsModel(ITargetService service)
	{
		_service = service;
	}

	public IListFeed<Targets> Targets => ListFeed.Async(async ct => await _service.GetAll(ct));
}

