
using Siemserva.Business.Models;

namespace Siemserva.Services.Target;

public class TargetService : ITargetService
{
	private List<Targets> Get()
	{
		var list = new List<Targets>
		{
			new(){ Domains = []},
			new(){ Domains = []},
			new(){ Azure = []},
			new(){ AzureSubscriptions = []},
			new(){ Mac = []},
			new(){ Linux = []}
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
