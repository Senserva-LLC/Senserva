
using Siemserva.Business.Models;
namespace Siemserva.Services.Target;

public interface ITargetService
{
	public Task<IImmutableList<Targets>> GetAll(CancellationToken ct);
	public Task<IImmutableList<Targets>> GetAzure(CancellationToken ct);
	public Task<IImmutableList<Targets>> GetWindows(CancellationToken ct);
	public Task<IImmutableList<Targets>> GetMac(CancellationToken ct);
	public Task<IImmutableList<Targets>> GetLinux(CancellationToken ct);
	public Task<IImmutableList<Targets>> GetIpRange(CancellationToken ct);

}

