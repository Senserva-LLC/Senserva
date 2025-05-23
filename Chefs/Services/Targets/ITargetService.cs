
using Siemserva.Business.Models;
namespace Siemserva.Services.Target;

public interface ITargetService
{
	public Task<IImmutableList<Targets>> GetAll(CancellationToken ct);
	public Task<IImmutableList<AzureTenant>> GetAzureTenants(CancellationToken ct);
	public Task<IImmutableList<AzureSubscription>> GetAzureSubscriptions(CancellationToken ct);
	public Task<IImmutableList<WindowsDirectory>> GetWindowsDirectories(CancellationToken ct);
	public Task<IImmutableList<WindowsWorkgroup>> GetWindowsWorkgroups(CancellationToken ct);
	public Task<IImmutableList<Mac>> GetMac(CancellationToken ct);
	public Task<IImmutableList<Linux>> GetLinux(CancellationToken ct);
	public Task<IImmutableList<IPRange>> GetIpRanges(CancellationToken ct);

}

