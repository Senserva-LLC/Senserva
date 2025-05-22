
using Siemserva.Business.Models;
using Siemserva.Services.Targets;

namespace Simeserva.Presentation;

public partial record TargetModel
{
	private readonly ITargetService _service;

	public TargetModel(ITargetService service)
	{
		_service = service;
	}

	public IListFeed<Target> Targets => ListFeed.Async(async ct => await _service.GetAll(ct));
}

