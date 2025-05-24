namespace Simeserva.Presentation;

public partial record FavoriteTechniquesModel
{
	private readonly INavigator _navigator;
	private readonly ITechniqueService _techniqueService;
	private readonly IMessenger _messenger;


	public FavoriteTechniquesModel(
		INavigator navigator,
		ITechniqueService recipeService,
		IMessenger messenger)
	{
		_navigator = navigator;
		_techniqueService = recipeService;
		_messenger = messenger;
	}

	public IListState<Technique> FavoriteTechniques => _techniqueService.FavoritedTechniques;
}
