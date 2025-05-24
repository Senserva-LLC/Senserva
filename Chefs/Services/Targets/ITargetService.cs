
using Siemserva.Business.Models;
namespace Siemserva.Services.Target;

public interface ITargetService
{
	public Task<IImmutableList<string>> GetOverview(Technique technique, CancellationToken ct);
	public Task<IImmutableList<AzureTenant>> GetAzureTenants(Technique technique, CancellationToken ct);
	public Task<IImmutableList<AzureSubscription>> GetAzureSubscriptions(Technique technique, CancellationToken ct);
	public Task<IImmutableList<WindowsDirectory>> GetWindowsDirectories(Technique technique, CancellationToken ct);
	public Task<IImmutableList<WindowsWorkgroup>> GetWindowsWorkgroups(Technique technique, CancellationToken ct);
	public Task<IImmutableList<Mac>> GetMac(Technique technique, CancellationToken ct);
	public Task<IImmutableList<Linux>> GetLinux(Technique technique, CancellationToken ct);
	public Task<IImmutableList<IPRange>> GetIpRanges(Technique technique, CancellationToken ct);

}

