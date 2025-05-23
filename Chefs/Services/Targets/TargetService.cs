
using Siemserva.Business.Models;
using Windows.Graphics.Display;

namespace Siemserva.Services.Target;

public class TargetService : ITargetService
{
	public async Task<IImmutableList<string>> GetOverview(CancellationToken ct)
	{
		var data = GetFakeData();
		return GetOverviews(data).ToImmutableList();
	}

	/// <summary>
	/// concise overview of the targets
	/// TODO will need an ID to grab the right data
	/// </summary>
	/// <returns></returns>
	public IEnumerable<string> GetOverviews(List<Targets> data)
	{
		List<string> overviews = [];
		var tenants = 0;
		var domains = 0;
		var workGroups = 0;
		var mac = 0;
		var subscriptions = 0;
		var linux = 0;
		var ipRange = 0;

		foreach (var item in data)
		{
			if (item.Tenants.Count > 0)
			{
				++tenants;
			}
			if (item.Domains.Count > 0)
			{
				++domains;
			}
			if (item.WorkGroups.Count > 0)
			{
				++workGroups;
			}
			if (item.AzureSubscriptions.Count > 0)
			{
				++subscriptions;
			}
			if (item.Macs.Count > 0)
			{
				++mac;
			}
			if (item.Linuxcies.Count > 0)
			{
				++linux;
			}
			if (item.IPRanges.Count > 0)
			{
				++ipRange;
			}
		}
		if (tenants > 0)
		{
			overviews.Add($"Azure Tenants: {tenants}");
		}
		if (domains > 0)
		{
			overviews.Add($"Domains: {domains}");
		}
		if (workGroups > 0)
		{
			overviews.Add($"WorkGroups: {workGroups}");
		}
		if (subscriptions > 0)
		{
			overviews.Add($"Azure Subscriptions: {subscriptions}");
		}
		if (mac > 0)
		{
			overviews.Add($"Macs: {mac}");
		}
		if (linux > 0)
		{
			overviews.Add($"Linux: {linux}");
		}
		if (ipRange > 0)
		{
			overviews.Add($"IP Ranges: {ipRange}");
		}
		return overviews;
	}

	/// <summary>
	/// this will become the json input
	/// </summary>
	/// <returns></returns>
	private List<Targets> GetFakeData()
	{
		var list = new List<Targets>
		{
			new(){ Domains = [new WindowsDirectory("w1", "my Windows AD", "id1", new SenservaCredentionals("bob")), new WindowsDirectory("w1a", "my Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ Domains = [new WindowsDirectory("d1", "your Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ Domains = [new WindowsDirectory("d1", "your Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ Domains = [new WindowsDirectory("d1", "your Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ Domains = [new WindowsDirectory("d1", "your Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ Domains = [new WindowsDirectory("d1", "your Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ Domains = [new WindowsDirectory("d1", "your Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ Domains = [new WindowsDirectory("d1", "your Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ Domains = [new WindowsDirectory("d1", "your Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ Domains = [new WindowsDirectory("d1", "your Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ Domains = [new WindowsDirectory("d1", "your Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ Domains = [new WindowsDirectory("d1", "your Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ Domains = [new WindowsDirectory("d1", "your Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ Domains = [new WindowsDirectory("d1", "your Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ Domains = [new WindowsDirectory("d1", "your Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ Domains = [new WindowsDirectory("d1", "your Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ Domains = [new WindowsDirectory("d1", "your Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ Domains = [new WindowsDirectory("d1", "your Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ Tenants = [new AzureTenant("a1", "my Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ AzureSubscriptions = [new AzureSubscription("a1", "my Windows AD", "id1", new SenservaCredentionals("bob"))]},
			new(){ IPRanges = [new IPRange("100.100", "my Windows AD", new SenservaCredentionals("bob"))]},
			new(){ AzureSubscriptions = [new AzureSubscription("a1", "use me if you have extra cash and time", "id1", new SenservaCredentionals("bob"))]},
			new(){ Macs = [new Mac("a1", "Use me if you have extra cash", new SenservaCredentionals("bob"))]},
			new(){ Linuxcies = [new Linux("a1", "use me if you have extra time", new SenservaCredentionals("bob"))]}
		};
		return list;
	}

	public async Task<IImmutableList<AzureTenant>> GetAzureTenants(CancellationToken ct)
	{
		var list = new List<AzureTenant>
		{
		};

		foreach (var target in GetFakeData())
		{
			if (target.Tenants?.Count > 0)
			{
				foreach (var azure in target.Tenants)
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

		foreach (var target in GetFakeData())
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
		foreach (var target in GetFakeData())
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
		foreach (var target in GetFakeData())
		{
			if (target.WorkGroups?.Count > 0)
			{
				foreach (var workgroup in target.WorkGroups)
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

		foreach (var target in GetFakeData())
		{
			if (target.Linuxcies?.Count > 0)
			{
				foreach (var linux in target.Linuxcies)
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

		foreach (var target in GetFakeData())
		{
			if (target.Macs?.Count > 0)
			{
				foreach (var mac in target.Macs)
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

		foreach (var target in GetFakeData())
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
