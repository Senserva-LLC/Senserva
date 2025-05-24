using Siemserva.Business.Models;

namespace Siemserva.Services.Target;

public class TargetService : ITargetService
{

	public async Task<IImmutableList<Policy>> GetPolicies(Technique technique, CancellationToken ct)
	{
		return new List<Policy>() { new Policy() }.ToImmutableList();
	}

	public async Task<IImmutableList<string>> GetOverview(Technique technique, CancellationToken ct)
	{
		var data = GetFakeData();
		return GetOverviews(technique, data).ToImmutableList();
	}

	/// <summary>
	/// concise overview of the targets
	/// TODO will need an ID to grab the right data
	/// </summary>
	/// <returns></returns>
	public IEnumerable<string> GetOverviews(Technique technique, List<Targets> data)
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
			new(){ Tenants = [new AzureTenant("t1", "my tenant", "id1", new SenservaCredentionals("bob"))]},
			new(){ AzureSubscriptions = [new AzureSubscription("s1", "my subscription", "id1", new SenservaCredentionals("bob"))]},
			new(){ IPRanges = [new IPRange("100.100", "my IPRanges", new SenservaCredentionals("bob"))]},
			new(){ AzureSubscriptions = [new AzureSubscription("s2", "subscription", "id2", new SenservaCredentionals("bob"))]},
			new(){ Macs = [new Mac("a1", "Use me if you have extra cash", new SenservaCredentionals("bob"))]},
			new(){ Linuxcies = [new Linux("a1", "use me if you have extra time", new SenservaCredentionals("bob"))]}
		};
		return list;
	}

	public async Task<IImmutableList<AzureTenant>> GetAzureTenants(Technique technique, CancellationToken ct)
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
	public async Task<IImmutableList<AzureSubscription>> GetAzureSubscriptions(Technique technique, CancellationToken ct)
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

	public async Task<IImmutableList<WindowsDirectory>> GetWindowsDirectories(Technique technique, CancellationToken ct)
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
	public async Task<IImmutableList<WindowsWorkgroup>> GetWindowsWorkgroups(Technique technique, CancellationToken ct)
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
	public async Task<IImmutableList<Linux>> GetLinux(Technique technique, CancellationToken ct)
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

	public async Task<IImmutableList<Mac>> GetMac(Technique technique, CancellationToken ct)
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

	public async Task<IImmutableList<IPRange>> GetIpRanges(Technique technique, CancellationToken ct)
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
