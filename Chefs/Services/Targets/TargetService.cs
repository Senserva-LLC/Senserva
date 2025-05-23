
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
			new(){ Azure = [new SenservaTenant("a1", "my Windows AD", "id1", new SenservaCredentionals("bob"))]},
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

	public async Task<IImmutableList<Targets>> GetAzure(CancellationToken ct)
	{
		var list = new List<Targets>
		{
		};

		foreach (var target in Get())
		{
			if (target.Azure?.Count > 0)
			{
				list.Add(target);
			}
			else if (target.AzureSubscriptions?.Count > 0)
			{
				list.Add(target);
			}
		}

		return list.ToImmutableList();
	}

	public async Task<IImmutableList<Targets>> GetLinux(CancellationToken ct)
	{
		var list = new List<Targets>
		{
		};

		foreach (var target in Get())
		{
			if (target.Linux?.Count > 0)
			{
				list.Add(target);
			}
		}

		return list.ToImmutableList();
	}

	public async Task<IImmutableList<Targets>> GetMac(CancellationToken ct)
	{
		var list = new List<Targets>
		{
		};

		foreach (var target in Get())
		{
			if (target.Mac?.Count > 0)
			{
				list.Add(target);
			}
		}

		return list.ToImmutableList();
	}

	public async Task<IImmutableList<Targets>> GetWindows(CancellationToken ct)
	{
		var list = new List<Targets>
		{
		};

		foreach (var target in Get())
		{
			if (target.Domains?.Count > 0)
			{
				list.Add(target);
			}
			else if (target.WorkGroup?.Count > 0)
			{
				list.Add(target);
			}
		}

		return list.ToImmutableList();
	}

	public async Task<IImmutableList<Targets>> GetIpRange(CancellationToken ct)
	{
		var list = new List<Targets>
		{
		};

		foreach (var target in Get())
		{
			if (target.IPRanges?.Count > 0)
			{
				list.Add(target);
			}
		}

		return list.ToImmutableList();
	}
}
