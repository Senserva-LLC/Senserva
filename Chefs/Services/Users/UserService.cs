using Chefs.Services.Clients;

namespace Chefs.Services.Users;

public class UserService(
	IWritableOptions<Credentials> credentialOptions)
	: IUserService
{
	private readonly IWritableOptions<Credentials> _credentialOptions = credentialOptions;

	private IState<SenservaUser> _user => State.Async(this, async ct => await GetCurrent(ct));

	public IFeed<SenservaUser> User => _user;

	public async Task<IImmutableList<SenservaUser>> GetPopularCreators(CancellationToken ct)
	{
		return new List<SenservaUser>().ToImmutableList();
	}

	public async Task<SenservaUser> GetCurrent(CancellationToken ct)
	{
		return new SenservaUser();
	}

	public async Task<SenservaUser> GetById(Guid userId, CancellationToken ct)
	{
		return new SenservaUser();
	}

	public async ValueTask Update(SenservaUser user, CancellationToken ct)
	{
		await _user.UpdateAsync(_ => user, ct);
	}

	//In case we need to add auth
	//public async ValueTask<bool> BasicAuthenticate(string email, string password, CancellationToken ct)
	//{
	//    var autentication = await _userEndpoint.Authenticate(email, password, ct);
	//    if (autentication)
	//    {
	//        await _credentialOptions.UpdateAsync(_ => new Credentials()
	//        {
	//            Email = email,
	//            SaveCredentials = true
	//        });

	//        return true;
	//    }

	//    return false;
	//}
}
