
using Siemserva.Business.Models;

namespace Siemserva.Services.Target;

public class TargetService : ITargetService
{
	private List<Targets> Get()
	{
		var list = new List<Targets>
		{
			new(){ Domains = [new WindowsDirectory("a1", "my Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ Domains = [new WindowsDirectory("a1", "your Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ Tenant = [new AzureTenant("a1", "my Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ AzureSubscriptions = [new AzureSubscription("a1", "my Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ IPRanges = [new IPRange("100.100", "my Windows AD", new SenservaCredentionals("bob"))]},
			new(){ AzureSubscriptions = [new AzureSubscription("a1", "use me if you have extra cash and time", "id1", new SenservaCredentionals("bob"))]},
			new(){ Mac = [new Mac("a1", "Use me if you have extra cash", new SenservaCredentionals("bob"))]},
			new(){ Linux = [new Linux("a1", "use me if you have extra time", new SenservaCredentionals("bob"))]}
		};
		return list;
	}

	public async Task<IImmutableList<Targets>> GetAll(CancellationToken ct)
	{
		return Get().ToImmutableList();
	}

	public async Task<IImmutableList<AzureTenant>> GetAzureTenants(CancellationToken ct)
	{
		var list = new List<AzureTenant>
		{
		};

		foreach (var target in Get())
		{
			if (target.Tenant?.Count > 0)
			{
				foreach (var azure in target.Tenant)
				{
					list.Add(azure);
				}
			}
		}

		return list.ToImmutableList();
	}
	public async Task<IImmutableList<AzureSubscription>> GetAzureSubscriptions(CancellationToken ct)
	{
		var list = new List<AzureSubscription>
		{
		};

		foreach (var target in Get())
		{
			if (target.AzureSubscriptions?.Count > 0)
			{
				foreach (var azure in target.AzureSubscriptions)
				{
					list.Add(azure);
				}
			}
		}

		return list.ToImmutableList();
	}

	public async Task<IImmutableList<WindowsDirectory>> GetWindowsDirectories(CancellationToken ct)
	{
		var list = new List<WindowsDirectory>
		{
		};
		foreach (var target in Get())
		{
			if (target.Domains?.Count > 0)
			{
				foreach (var domain in target.Domains)
				{
					list.Add(domain);
				}
			}
		}
		return list.ToImmutableList();
	}
	public async Task<IImmutableList<WindowsWorkgroup>> GetWindowsWorkgroups(CancellationToken ct)
	{
		var list = new List<WindowsWorkgroup>
		{
		};
		foreach (var target in Get())
		{
			if (target.WorkGroup?.Count > 0)
			{
				foreach (var workgroup in target.WorkGroup)
				{
					list.Add(workgroup);
				}
			}
		}
		return list.ToImmutableList();
	}
	public async Task<IImmutableList<Linux>> GetLinux(CancellationToken ct)
	{
		var list = new List<Linux>
		{
		};

		foreach (var target in Get())
		{
			if (target.Linux?.Count > 0)
			{
				foreach (var linux in target.Linux)
				{
					list.Add(linux);
				}
			}
		}

		return list.ToImmutableList();
	}

	public async Task<IImmutableList<Mac>> GetMac(CancellationToken ct)
	{
		var list = new List<Mac>
		{
		};

		foreach (var target in Get())
		{
			if (target.Mac?.Count > 0)
			{
				foreach (var mac in target.Mac)
				{
					list.Add(mac);
				}
			}
		}

		return list.ToImmutableList();
	}

	public async Task<IImmutableList<IPRange>> GetIpRanges(CancellationToken ct)
	{
		var list = new List<IPRange>
		{
		};

		foreach (var target in Get())
		{
			if (target.IPRanges?.Count > 0)
			{
				foreach (var range in target.IPRanges)
				{
					list.Add(range);
				}
			}
		}

		return list.ToImmutableList();
	}
}
