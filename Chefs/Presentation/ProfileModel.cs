namespace Chefs.Presentation;

public partial record ProfileModel
{
	private readonly ITechniqueService _recipeService;
	private readonly INavigator _navigator;

	public ProfileModel(
		INavigator navigator,
		ITechniqueService recipeService,
		IUserService userService,
		SenservaUser? user)
	{
		_navigator = navigator;
		_recipeService = recipeService;

		Profile = user != null ? State.Value(this, () => user) : userService.User;
	}

	public IFeed<SenservaUser> Profile { get; }

	public IListFeed<Technique> Techniques => Profile
		.SelectAsync(async (user, ct) => await _recipeService.GetByUser(user.Id, ct))
		.AsListFeed();
}
