
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

	public IListFeed<AzureTenant> Azure => ListFeed.Async(async ct => await _service.GetAzureTenants(ct));

	public IListFeed<WindowsDirectory> Domains => ListFeed.Async(async ct => await _service.GetWindowsDirectories(ct));

	public IListFeed<Mac> Mac => ListFeed.Async(async ct => await _service.GetMac(ct));

	public IListFeed<Linux> Linux => ListFeed.Async(async ct => await _service.GetLinux(ct));

	public IListFeed<IPRange> IPRange => ListFeed.Async(async ct => await _service.GetIpRanges(ct));
}

