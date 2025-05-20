namespace Chefs.Business.Models;

public partial record SenservaUser
{

	public SenservaUser()
	{
		Id = Guid.NewGuid();
		IsCurrent = true;
		FullName = "JJHS";
	}

	public Guid Id { get; init; }
	public string? UrlProfileImage { get; init; }
	public string? FullName { get; init; }
	public string? Description { get; init; }
	public string? Email { get; init; }
	public string? PhoneNumber { get; init; }
	public long? Techniques { get; init; }
	public bool IsCurrent { get; init; }
}
