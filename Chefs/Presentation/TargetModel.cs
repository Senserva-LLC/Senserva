
using Siemserva.Business.Models;
using Siemserva.Services.Target;

namespace Simeserva.Presentation;

//https://platform.uno/docs/articles/external/uno.extensions/doc/Learn/Tutorials/Navigation/HowTo-NavigateInXAML.html
public partial record TargetsModel
{
	private readonly ITargetService _service;
	private readonly Technique _technique;

	public TargetsModel(Technique technique, ITargetService service)
	{
		_technique = technique;
		_service = service;
	}

	/// <summary>
	///  all targets
	/// </summary>

	public IListFeed<string> Overviews => ListFeed.Async(async ct => await _service.GetOverview(_technique, ct));

	public IListFeed<Policy> Policies => ListFeed.Async(async ct => await _service.GetPolicies(_technique, ct));

	//public IListFeed<AzureTenant> TEST => Profile
	//	.SelectAsync(async (user, ct) => await _service.GetByUser(user.Id, ct))
	//	.AsListFeed();

	public IListFeed<AzureTenant> AzureTenants => ListFeed.Async(async ct => await _service.GetAzureTenants(_technique, ct));

	public IListFeed<AzureSubscription> AzureSubscriptions => ListFeed.Async(async ct => await _service.GetAzureSubscriptions(_technique, ct));

	public IListFeed<WindowsDirectory> Domains => ListFeed.Async(async ct => await _service.GetWindowsDirectories(_technique, ct));

	public IListFeed<WindowsWorkgroup> Workgroups => ListFeed.Async(async ct => await _service.GetWindowsWorkgroups(_technique, ct));

	public IListFeed<PC> PCs => ListFeed.Async(async ct => await _service.GetPC(_technique, ct));

	public IListFeed<Mac> Mac => ListFeed.Async(async ct => await _service.GetMac(_technique, ct));

	public IListFeed<Linux> Linux => ListFeed.Async(async ct => await _service.GetLinux(_technique, ct));

	public IListFeed<IPRange> IPRange => ListFeed.Async(async ct => await _service.GetIpRanges(_technique, ct));
}

