namespace Simeserva.Presentation;

public partial record ProfileModel
{
	private readonly ITechniqueService _techniqueService;
	private readonly INavigator _navigator;

	public ProfileModel(
		INavigator navigator,
		ITechniqueService recipeService,
		IUserService userService,
		SenservaUser? user)
	{
		_navigator = navigator;
		_techniqueService = recipeService;

		Profile = user != null ? State.Value(this, () => user) : userService.User;
	}

	public IFeed<SenservaUser> Profile { get; }

	public IListFeed<Technique> Techniques => Profile
		.SelectAsync(async (user, ct) => await _techniqueService.GetByUser(user.Id, ct))
		.AsListFeed();
}
