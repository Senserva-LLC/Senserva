
namespace Simeserva.Services.Cookbooks;

public class CookbookService(IMessenger messenger, IUserService userService)
	: ICookbookService
{
	public async ValueTask<Cookbook> Create(string name, IImmutableList<Technique> recipes, CancellationToken ct)
	{
		var currentUser = await userService.GetCurrent(ct);
		var cookbookData = Cookbook.CreateData(currentUser.Id, name, recipes);

		return cookbookData;
	}

	public async ValueTask<Cookbook> Update(Cookbook cookbook, IImmutableList<Technique> recipes, CancellationToken ct)
	{
		var newCookbook = new Cookbook(recipes);
		messenger.Send(new EntityMessage<Cookbook>(EntityChange.Updated, newCookbook));
		return newCookbook;
	}

	public async ValueTask Update(Cookbook cookbook, CancellationToken ct)
	{
		messenger.Send(new EntityMessage<Cookbook>(EntityChange.Updated, cookbook));
	}

	public async ValueTask Save(Cookbook cookbook, CancellationToken ct)
	{
		var currentUser = await userService.GetCurrent(ct);

		messenger.Send(new EntityMessage<Cookbook>(EntityChange.Created, cookbook));
	}

	public async ValueTask<IImmutableList<Cookbook>> GetSaved(CancellationToken ct)
	{
		return Get();
	}

	public async ValueTask<IImmutableList<Cookbook>> GetByUser(Guid userId, CancellationToken ct)
	{
		return Get();
	}

	internal IImmutableList<Cookbook> Get() => new List<Cookbook>().ToImmutableList();
}
