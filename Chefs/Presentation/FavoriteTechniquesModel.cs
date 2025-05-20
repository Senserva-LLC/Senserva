namespace Simeserva.Presentation;

public partial record FavoriteTechniquesModel
{
	private readonly INavigator _navigator;
	private readonly ITechniqueService _techniqueService;
	private readonly ICookbookService _cookbookService;
	private readonly IMessenger _messenger;


	public FavoriteTechniquesModel(
		INavigator navigator,
		ITechniqueService recipeService,
		ICookbookService cookbookService,
		IMessenger messenger)
	{
		_navigator = navigator;
		_techniqueService = recipeService;
		_cookbookService = cookbookService;
		_messenger = messenger;
	}

	public IListState<Cookbook> SavedCookbooks => ListState
		.Async(this, _cookbookService.GetSaved)
		.Observe(_messenger, cb => cb.Id);

	public IListState<Technique> FavoriteTechniques => _techniqueService.FavoritedTechniques;
}
