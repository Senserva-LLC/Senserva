
namespace Siemserva.Business.Models;

/// <summary>
/// how do we pass these securely? just by name and then
/// we get the name from the vault
/// expect this will be different for different platforms so it
/// will become a virtual class
/// </summary>
/// <param name="Name"></param>
public class SenservaCredentionals(string Name);

/// <summary>
///  build out as needed
/// </summary>
/// <param name="Name"></param>
/// <param name="Id"></param>
public partial record SenservaTenant(string Name, string Description, string Id, SenservaCredentionals Credentionals);

public partial record AzureSubscription(string Name, string Description, string Id, SenservaCredentionals Credentionals);

public partial record SenservaManagementGroup(string Name, string Description, string Id, SenservaCredentionals Credentionals);

public partial record WindowsDirectory(string Name, string Description, string Id, SenservaCredentionals Credentionals);

public partial record WindowsWorkgroup(string Name, string Description, SenservaCredentionals Credentionals);

public partial record Mac(string Name, string Description, SenservaCredentionals Credentionals);

public partial record Linux(string Name, string Description, SenservaCredentionals Credentionals);

/// https://github.com/jsakamoto/ipaddressrange/
public partial record IPRange(string Range, string Description, SenservaCredentionals Credentionals);

/// <summary>
/// domain, ip range, ip address, machine name
/// TODO figure out creds when on desktop etc
/// https://learn.microsoft.com/en-us/entra/fundamentals/entra-admin-center
/// </summary>
public partial record Targets : ISenservaEntity
{
	public Guid Id { get; init; }
	public string Name { get; init; }
	public string Description { get; init; }

	public List<IPRange> IPRanges;
	public List<WindowsDirectory> Domains;
	public List<WindowsWorkgroup> WorkGroup;
	public List<SenservaTenant> Azure;
	public List<AzureSubscription> AzureSubscriptions;
	public List<Mac> Mac;
	public List<Linux> Linux;

	public Targets()
	{
		Id = Guid.NewGuid();
		Name = "test2";
		Description = "Best darn thing ever!";
	}
}
