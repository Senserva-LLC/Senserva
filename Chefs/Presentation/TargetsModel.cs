
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

	/// <summary>
	///  all targets
	/// </summary>
	public IListFeed<Targets> Targets => ListFeed.Async(async ct => await _service.GetAll(ct));

	public IListFeed<Targets> Azure => ListFeed.Async(async ct => await _service.GetAzure(ct));

	public IListFeed<Targets> Windows => ListFeed.Async(async ct => await _service.GetWindows(ct));

	public IListFeed<Targets> Mac => ListFeed.Async(async ct => await _service.GetMac(ct));

	public IListFeed<Targets> Linux => ListFeed.Async(async ct => await _service.GetLinux(ct));

	public IListFeed<Targets> IPRange => ListFeed.Async(async ct => await _service.GetIpRange(ct));
}

